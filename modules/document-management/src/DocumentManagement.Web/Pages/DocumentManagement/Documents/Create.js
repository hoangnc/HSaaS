(function () {
    const datePattern = abp.localization.currentCulture.dateTimeFormat.shortDatePattern;
    toastr.options.positionClass = 'toast-top-right';

    toastr.options.onHidden = function () {
        window.location.href = `/DocumentManagement/documents`;
    }

    let $Code = $('#Code');
    let $Approver = $('#Approver');
    let $Drafter = $('#Drafter');
    let $Auditor = $('#Auditor');
    let $CompanyCode = $('#CompanyCode');
    let $DepartmentCode = $('#DepartmentCode');
    let $CompanyName = $('#CompanyName');
    let $DepartmentName = $('#DepartmentName');
    let $DocumentType = $('#DocumentType');
    let $AppliedToEntire = $('#AppliedToEntire');
    let $IssuedToEntire = $('#IssuedToEntire');
    let $Module = $('#Module');
    let $IssuedStatusId = $('#IssuedStatusId');
    let $StatusId = $('#StatusId');
    let $OneYear = $('#OneYear');
    let $TwoYear = $('#TwoYear');
    let $btnSelectAll = $('#btnSelectAll');
    let $btnDeselectAll = $('#btnDeselectAll');
    let $fileupload = $('#fileupload');
    let $EffectiveDate = $('#EffectiveDate');
    let $ReviewDate = $('#ReviewDate');

    let departments = [
    ];

    let userDepartments = [
    ];

    let documentTypes = [
    ];

    let users = [
    ];

    let companies = [
    ];

    let groups = [
    ];

    let modules = [
    ];

    let statuses = [
        { text: 'Input', id: 1 },
        { text: 'Submit', id: 2 },
        { text: 'Confirm', id: 3 },
        { text: 'Approve', id: 4 }
    ];

    let issuedStatuses = [
        { text: 'Đã ban hành', id: 1 },
        { text: 'Đang chờ phê duyệt', id: 2 }
    ];

    let appendixDocuments = [{
        id: 1,
        name: '',
        code: '',
        documentNumber: '',
        reviewNumber: '',
        fileName: '',
    }];

    let appendixFiles = [];
    let storedFiles = [];

    let grdAppendixDocuments;
    let grdRelateToDocuments;

    const genDocumentCode = () => {
        return 'DM' + moment().format('Ymdhmmss');;
    }

    const getFileExtension = (filename) => {
        const ext = /^.+\.([^.]+)$/.exec(filename);
        return ext == null ? "" : ext[1];
    }

    const objectToFormData = (obj, formData, rootName, ignoreList) => {

        const appendFormData = (data, root) => {
            if (!ignore(root)) {
                root = root || '';
                if (data instanceof File) {
                    formData.append(root, data);
                } else if (Array.isArray(data)) {
                    for (let i = 0; i < data.length; i++) {
                        appendFormData(data[i], root + '[' + i + ']');
                    }
                } else if (typeof data === 'object' && data) {
                    for (let key in data) {
                        if (data.hasOwnProperty(key)) {
                            if (root === '') {
                                appendFormData(data[key], key);
                            } else {
                                appendFormData(data[key], root + '.' + key);
                            }
                        }
                    }
                } else {
                    if (data !== null && typeof data !== 'undefined') {
                        formData.append(root, data);
                    }
                }
            }
        }

        const ignore = (root) => {
            return Array.isArray(ignoreList)
                && ignoreList.some(function (x) { return x === root; });
        }

        appendFormData(obj, rootName);
    }

    const formatFileSize = (bytes) => {
        if (typeof bytes !== 'number') {
            return '';
        }
        if (bytes >= 1000000000) {
            return (bytes / 1000000000).toFixed(2) + ' GB';
        }
        if (bytes >= 1000000) {
            return (bytes / 1000000).toFixed(2) + ' MB';
        }
        return (bytes / 1000).toFixed(2) + ' KB';
    }

    const l = abp.localization.getResource('DocumentManagement');
    const createAndReleaseDocumentApiPath = '/api/document-management/documents/create-and-release';
    const createDocumentApiPath = '/api/document-management/documents/create';
    const getCompaniesApiPath = '/api/master-data/companies';

    const _documentAppService = documentManagement.document.document;
    const _companyAppService = masterData.companies.company;
    const _departmentAppService = masterData.departments.department;
    const _documentTypeAppService = masterData.documentTypes.documentType;
    const _moduleAppService = masterData.modules.module;
    const _identityUserAppService = volo.abp.identity.identityUser;
    const _userDepartmentAppService = masterData.userDepartments.userDepartment;
    
    const _documentLookupModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'DocumentManagement/Documents/DocumentLookup',
        modalClass: 'DocumentLookup'
    });
    
    const uploadTemplate = tmpl('template-upload');

    const renderTemplate = (func, files) => {
        if (!func) {
            return $();
        }

        const result = func({
            files: files,
            formatFileSize: this._formatFileSize,
            options: this.options
        });

        if (result instanceof $) {
            return result;
        }

        storedFiles = files;
        return $('.files').html(result).children();
    }

    const renderUpload = (files) => {
        return renderTemplate(
            uploadTemplate,
            files
        );
    }

    const deleteHandler = (e) => {
        e.preventDefault();
        const tr = $(this).closest('tr');
        let fileName = tr.find('p[name=fileName]').data("file");

        for (let i = 0; i < storedFiles.length; i++) {
            if (storedFiles[i].name === fileName) {
                storedFiles.splice(i, 1);
                tr.remove();
                break;
            }
        }
    }  

    const updateAppendixFile = (fileName, appendixName) => {
        const index = appendixFiles.findIndex(f => {
            return f.file.name === fileName;
        });

        if (index >= 0) {
            appendixFiles[index].appendixName = appendixName;
        }
    }

    const addFileToAppendixFiles = (file, appendixName) => {
        const index = appendixFiles.findIndex(f => {
            return f.appendixName === appendixName;
        });

        if (index >= 0) {
            appendixFiles[index] = {
                appendixName: appendixName,
                file: file
            };
        } else {
            appendixFiles.push({
                appendixName: appendixName,
                file: file
            });
        }
    }

    const isExistingAppendix = (appendixName, currentRowIndex) => {
        let index = -1;
        let rows = grdAppendixDocuments.getRows();

        for (let i = 0; i < rows.length; i++) {
            let rowData = rows[i].getData();
            if (i !== currentRowIndex && rowData.name === appendixName) {
                index = i;
                break;
            }
        }

        return index >= 0;
    }

    const getFileAppendix = (appendixName) => {

        const index = appendixFiles.findIndex(f => {
            return f.appendixName === appendixName;
        });

        return appendixFiles[index];
    }

    const getAppendixes = () => {

        const appendixes = grdAppendixDocuments.getData().filter(function (d) {
            if (d.name) {
                return true;
            }
            return false;
        }).map(d => d);

        return appendixes;
    }

    const getRelateToDocuments = () => {
        const documents = grdRelateToDocuments.getData().filter(function (d) {
            if (d.name) {
                return true;
            }
            return false;
        }).map(d => d);

        return documents;
    }

    const formatUserResult = (user) => {
        if (!user.id)
            return user.text;

        const text = user.text;
        const fullName = text;
        const department = user.departmentName;

        return $(`<span>${fullName}</span><div><small style="color: #a5a0a0">${l('DepartmentName')}: ${department}</small></div>`);
    }

    const formatUserSelection = (user) => {
        if (!user.id)
            return user.text;

        const text = user.text;

        return $(`<span>${text}</span>`);
    }

    const formatGroupSelection = (group) => {
        if (!group.id)
            return group.text;

        const text = group.text;

        return $(`<span>${text}</span>`);
    }

    const formatGroupResult = (group) => {
        if (!group.id)
            return group.text;

        const text = group.text;

        return $(`<span>${text}</span><div><small style="color: #a5a0a0">Email: ${group.email}</small></div>`);
    }

    const getCompanies = () => {
        return _companyAppService.getList({filter: '', skipCount: 0, maxResultCount: 100})
            .then((data => {
                companies = data.items.map((company) => {
                    return { id: company.code, text: company.name };
                });
            }));
    }

    const getDepartments  = () => {
        return _departmentAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                departments = data.items.map((department) => {
                    return { id: department.code, text: department.name, email: department.emailAddress };
                });
            }));
    }

    const getUserDepartments = () => {
        return _userDepartmentAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                userDepartments = data.items.map((department) => {
                    return department;
                });
            }));
    }

    const getDocumentTypes = () => {
        return _documentTypeAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                documentTypes = data.items.map((documentType) => {
                    return { id: documentType.code, text: documentType.name };
                });
            }));
    }

    const getModules = () => {
        return _moduleAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                modules = data.items.map((module) => {
                    return { id: module.code, text: module.name };
                });
            }));
    }

    const getUsers = () => {     
            _identityUserAppService.getList({ filter: '', skipCount: 0, maxResultCount: 1000 })
                .then((data => {
                    users = data
                        .items
                        .filter((user) => user.userName !== 'admin')
                        .map((user) => {
                            return { id: user.userName, text: `${user.surname} ${user.name}`, departmentName: 'Announce' };
                        });
                }));
    }

    const getDocuments = async () => {
        await _documentAppService.getList({ filter: '', page: 1, size: 10000 })
            .then((data => {
                documents = data.items.map((document) => {
                    return { id: document.code, text: document.name };
                });
            }));
    }

    $(function () {

        getDocuments();

        let selectedRelateToDocumentRow;

        const initGrdRelateToDocuments = function () {        

            grdRelateToDocuments = new Tabulator("#grdRelateToDocuments", {
                height: "311px",
                layout: "fitColumns",
                selectable: 1,
                movableRows: true,
                resizableRows: true, //allow row order to be changed
                tooltips: true,
                history: true,
                index: "id",
                columns: [
                    {
                        title: 'Stt',
                        formatter: "rownum",
                        hozAlign: "center",
                        width: 80,
                        frozen: true
                    },
                    {
                        title: l("Name"),
                        field: "name",
                        hozAlign: "left",
                        validator: "required",
                        frozen: true,
                        cellDblClick: function (e, cell) {
                            selectedRelateToDocumentRow = cell.getRow();

                            _documentLookupModal.open({
                                documentName: cell.getValue()
                            });
                        },
                        width: 200
                    },
                    {
                        title: 'Files',
                        field: "fileName",
                        hozAlign: "left",
                        width: 300,
                        frozen: true,
                        formatter: function (cell, formatterParams, onRendered) {
                            let html = '';

                            if (cell.getValue()) {
                                const fileNames = cell.getValue().split(';');
                                const folderName = cell.getRow().getData().folderName;

                                if (fileNames
                                    && folderName) {
                                    if (fileNames.length > 0) {

                                        for (let index = 0; index < fileNames.length; index++) {
                                            if (fileNames[index]) {
                                                const filePath = `${folderName}/${fileNames[index]}`;
                                                let fileIcon = 'fa fa-file-o';
                                                switch (getFileExtension(fileNames[index])) {
                                                    case 'pdf':
                                                        fileIcon = 'fa fa-file-pdf-o';
                                                        break;
                                                    case 'xls':
                                                        fileIcon = 'fa fa-file-excel-o';
                                                        break;
                                                    case 'xlsx':
                                                        fileIcon = 'fa fa-file-excel-o';
                                                        break;
                                                    case 'doc':
                                                        fileIcon = 'fa fa-file-word-o';
                                                        break;
                                                    case 'docx':
                                                        fileIcon = 'fa fa-file-word-o';
                                                        break;
                                                }
                                                html = html + `<a target="_blank" href="/downloadfile/view-file?sourceDoc=${filePath}"> <i class="${fileIcon}"></i> ${fileNames[index]}</a><br>`;
                                            }
                                        }
                                    }
                                }
                            }
                            return html;
                        }
                    },
                    {
                        title: l('DocumentNumber'),
                        field: "reviewNumber",
                        hozAlign: "left",
                        width: 200
                    },
                    {
                        title: l('EffectiveDate'),
                        field: "effectiveDate",
                        formatter: "datetime",
                        formatterParams: {
                            outputFormat: datePattern.toUpperCase(),
                            invalidPlaceholder: "",
                        },
                        hozAlign: "left",
                        width: 200
                    },
                    {
                        title: l('ReviewDate'),
                        field: "reviewDate",
                        formatter: "datetime",
                        formatterParams: {
                            outputFormat: datePattern.toUpperCase(),
                            invalidPlaceholder: "",
                        },
                        hozAlign: "left",
                        width: 200
                    },
                    {
                        title: '',
                        field: "code",
                        hozAlign: "left",
                        visible: false
                    },
                    {
                        title: '',
                        field: "folderName",
                        hozAlign: "left",
                        visible: false
                    }
                ],
            });
        }

        //Add row on "Add Row" button click
        document.getElementById("btnAddRelateToDocuments").addEventListener("click", () => {
            //TODO: define the localization of field name
            grdRelateToDocuments.addRow({ name: 'Please choose document..'});
        });

        //Delete row on "Delete Row" button click
        document.getElementById("btnRemoveRelateToDocuments").addEventListener("click", () => {
            const selectedRows = grdRelateToDocuments.getSelectedRows();
            if (selectedRows && selectedRows.length > 0)
                grdRelateToDocuments.deleteRow(selectedRows[0]);
        });

        _documentLookupModal.onResult((selectedDocuments) => {
            if (selectedRelateToDocumentRow) {
                if (selectedDocuments && selectedDocuments instanceof Array) {
                    if (selectedDocuments.length > 0) {
                        selectedRelateToDocumentRow.update(selectedDocuments[0]);
                    }
                }
            }
        });

        initGrdRelateToDocuments();
    });

    $(function () {

        const fileEditor = function (cell, onRendered, success, cancel, editorParams) {
            //cell - the cell component for the editable cell
            //onRendered - function to call when the editor has been rendered
            //success - function to call to pass the successfuly updated value to Tabulator
            //cancel - function to call to abort the edit and return to a normal cell
            //editorParams - params object passed into the editorParams column definition property

            //create and style input
            input = document.createElement("input");

            input.setAttribute("type", "file");
            input.setAttribute("data-name", cell.getRow().getData().name);

            input.style.padding = "4px";
            input.style.width = "100%";
            input.style.boxSizing = "border-box";

            onRendered(function () {
                input.focus();
                input.style.height = "100%";
            });

            function onChange() {
                if (input.files[0]) {
                    if (input.files[0].name) {
                        addFileToAppendixFiles(input.files[0], $(this).attr('data-name'));
                        success(input.files[0].name);
                    }
                }
                else {
                    cancel();
                }
            }

            //submit new value on blur or change

            input.addEventListener("change", onChange);

            //submit new value on enter
            input.addEventListener("keydown", function (e) {
                if (e.keyCode == 13) {
                    onChange();
                }

                if (e.keyCode == 27) {
                    cancel();
                }
            });

            return input;
        };

        const initGrdAppendixDocuments = () => {

            grdAppendixDocuments = new Tabulator("#grdAppendixDocuments", {
                height: "311px",
                layout: "fitColumns",
                selectable: 1,
                history: true,
                columns: [
                    {
                        title: 'Stt',
                        formatter: "rownum",
                        hozAlign: "center",
                        width: 80
                    },
                    {
                        title: l("Name"),
                        field: "name",
                        hozAlign: "left",
                        editor: "input",
                        validator: "required"
                    },
                    {
                        title: l("File"),
                        field: "fileName",
                        editor: fileEditor,
                        editorParams: function (cell) {
                            const rowData = cell.getRow().getData();
                            return rowData;
                        }
                    },
                    {
                        title: l("ReviewNumber"),
                        field: "reviewNumber",
                        editor: 'input',
                        validator: "required",
                        hozAlign: "left",
                        width: 200
                    }
                ],
            });

            //undo button
            document.getElementById("appendix-history-undo").addEventListener("click", () => {
                grdAppendixDocuments.undo();
            });

            //redo button
            document.getElementById("appendix-history-redo").addEventListener("click", () => {
                grdAppendixDocuments.redo();
            });

            //Add row on "Add Row" button click
            document.getElementById("btnAddAppendixDocuments").addEventListener("click", () => {
                grdAppendixDocuments.addRow({});
            });

            //Delete row on "Delete Row" button click
            document.getElementById("btnRemoveAppendixDocuments").addEventListener("click", () => {
                const selectedRows = grdAppendixDocuments.getSelectedRows();
                if (selectedRows && selectedRows.length > 0)
                    grdAppendixDocuments.deleteRow(selectedRows[0]);
            });
        }

        initGrdAppendixDocuments();
    });

    $(function () {

        const create = ()  => {
            // Create an FormData object
            let formData = $("#formDocument").submit(function (e) {
                e.preventDefault();
                return;
            });

            //formData[0] contain form data only
            // You can directly make object via using form id but it require all ajax operation inside $("form").submit(<!-- Ajax Here   -->)
            formData = new FormData(formData[0]);

            // Document files
            for (let i = 0, len = storedFiles.length; i < len; i++) {
                formData.append('files', storedFiles[i]);
            }

            // Appendixs files
            for (let i = 0; i < appendixFiles.length; i++) {
                formData.append('appendixFiles', appendixFiles[i].file);
            }

            const appendixes = getAppendixes();
            objectToFormData(appendixes, formData, 'appendixes', []);

            if ($Approver.val())
                formData.set('Approver', $Approver.val().join(';'));

            if ($Auditor.val())
                formData.set('Auditor', $Auditor.val().join(';'));

            if ($Drafter.val())
                formData.set('Drafter', $Drafter.val().join(';'));

            if ($IssuedToEntire.val())
                formData.set('IssuedToEntire', $IssuedToEntire.val().join(';'));

            formData.set('Active', true);
            formData.set('DDCAudited', true);

            const relateToDocuments = getRelateToDocuments().map((document) => document.code);
            formData.set('RelateToDocuments', relateToDocuments.join(';'));

            abp.ajax({
                url: createDocumentApiPath,
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false
            }).done(function (data) {
                abp.notify.success(l(
                    'MessageCreateDocumentSuccessed'),
                    "Create Document");
            });
        }

        const createAndRelease = () => {
            // Create an FormData object
            let formData = $("#formDocument").submit(function (e) {
                e.preventDefault();
                return;
            });

            //formData[0] contain form data only
            // You can directly make object via using form id but it require all ajax operation inside $("form").submit(<!-- Ajax Here   -->)
            formData = new FormData(formData[0]);

            // Document files
            for (let i = 0, len = storedFiles.length; i < len; i++) {
                formData.append('files', storedFiles[i]);
            }

            // Appendixs files
            for (let i = 0; i < appendixFiles.length; i++) {
                formData.append('appendixFiles', appendixFiles[i].file);
            }

            const appendixes = getAppendixes();
            objectToFormData(appendixes, formData, 'appendixes', []);

            if ($Approver.val())
                formData.set('Approver', $Approver.val().join(';'));

            if ($Auditor.val())
                formData.set('Auditor', $Auditor.val().join(';'));

            if ($Drafter.val())
                formData.set('Drafter', $Drafter.val().join(';'));

            if ($IssuedToEntire.val())
                formData.set('IssuedToEntire', $IssuedToEntire.val().join(';'));

            formData.set('Active', true);
            formData.set('DDCAudited', true);

            const relateToDocuments = getRelateToDocuments().map((document) => document.code);
            formData.set('RelateToDocuments', relateToDocuments.join(';'));

            abp.ajax({
                url: createAndReleaseDocumentApiPath,
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false
            }).done(function (data) {
                abp.notify.success(l(
                    'MessageCreateDocumentSuccessed'),
                    "Create Document");
            });
        }

        $Code.val(genDocumentCode());

        $Approver.select2({
            data: users,
            templateResult: formatUserResult,
            templateSelection: formatUserSelection
        });

        $Drafter.select2({
            data: users,
            templateResult: formatUserResult,
            templateSelection: formatUserSelection
        });

        $Auditor.select2({
            data: users,
            templateResult: formatUserResult,
            templateSelection: formatUserSelection
        });

        $IssuedToEntire.select2({
            data: departments,
            templateResult: formatGroupResult,
            templateSelection: formatGroupSelection
        });

        $Module.select2({
            data: modules
        });

        $StatusId.select2({
            data: statuses
        });

        $IssuedStatusId.select2({
            data: issuedStatuses
        });

        Promise.all([
            getCompanies(),
            getDepartments(),
            getDocumentTypes(),
            getModules(),
            getUsers(),
            getUserDepartments()
        ]).then(() => {
            users = users.map(user => {
                let userDepartment = userDepartments.find(ud => ud.userName === user.id);
                
                if (userDepartment) {
                    let department = departments.find(d => d.id === userDepartment.departmentCode);

                    if (department) {
                        user.departmentName = department.text;
                    }
                }

                return user;
            });
            
            $CompanyCode.select2({
                data: companies
            });

            $Approver.select2({
                data: users,
                templateResult: formatUserResult,
                templateSelection: formatUserSelection
            });

            $Drafter.select2({
                data: users,
                templateResult: formatUserResult,
                templateSelection: formatUserSelection
            });

            $Auditor.select2({
                data: users,
                templateResult: formatUserResult,
                templateSelection: formatUserSelection
            });

            $DepartmentCode.select2({
                data: departments
            });

            $DocumentType.select2({
                data: documentTypes
            });

            $Module.select2({
                data: modules
            });

            $IssuedToEntire.select2({
                data: departments,
                templateResult: formatGroupResult,
                templateSelection: formatGroupSelection
            });

            initGrdRelateToDocuments();

            initGrdAppendixDocuments();
            // all requests finished successfully
        }).catch(() => {
            // all requests finished but one or more failed
        })

        $OneYear.click(function () {
            let date = moment($EffectiveDate.val(), datePattern, false).add(1, 'year');
            $ReviewDate.datepicker('setDate', date.toDate());
        });

        $TwoYear.click(function () {
            let date = moment($EffectiveDate.val(), datePattern, false).add(2, 'year');
            $ReviewDate.datepicker('setDate', date.toDate());
        });

        // Effective date
        $EffectiveDate.datepicker({
            language: abp.localization.currentCulture.cultureName,
            showOn: 'focus',
            autoclose: true
        });

        // Review date
        $ReviewDate.datepicker({
            language: abp.localization.currentCulture.cultureName,
            showOn: 'focus',
            autoclose: true
        });

        $btnSelectAll.click(function () {
            $IssuedToEntire.val(departments.map(g => { return g.id; })).trigger('change');;
        });

        $btnDeselectAll.click(function () {
            $IssuedToEntire.val([]).trigger('change');;
        });

        $fileupload.fileupload({
            autoUpload: false,
            disableImageResize: /Android(?!.*Chrome)|Opera/
                .test(window.navigator.userAgent),
            maxFileSize: 9990000,
            change: function (e, data) {
                renderUpload(data.files);
                $('button[name=deleteFile]').click(deleteHandler);
            },
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png|xls|pdf|xslx)$/i
        });

        $.validate({
            language: abp.localization.currentCulture.cultureName,
            form: '#formDocument',
            addValidClassOnAll: true
        });

        $("#formDocument").submit((e) => {
            e.preventDefault();
            let btn = $(this).find("button[type=submit]:focus");
            if (btn.length > 0) {
                if (btn[0].id === 'btnSubmit') {
                    create();
                } else {
                    createAndRelease();
                }
            }
        });
    });

})();