
const datePattern = abp.localization.currentCulture.dateTimeFormat.shortDatePattern;
moment.locale(abp.localization.currentCulture.cultureName);

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

let departments = [

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

let relatedDocuments = [];

let appendixFiles = [];
let storedFiles = [];

let grdAppendixDocuments;
let grdRelateToDocuments;

function getFileExtension(filename) {
    var ext = /^.+\.([^.]+)$/.exec(filename);
    return ext == null ? "" : ext[1];
}

function formatFileSize(bytes) {
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

(function () {
    const l = abp.localization.getResource('DocumentManagement');
    const _documentAppService = documentManagement.document.document;
    const _companyAppService = masterData.companies.company;
    const _departmentAppService = masterData.departments.department;
    const _documentTypeAppService = masterData.documentTypes.documentType;
    const _moduleAppService = masterData.modules.module;

    function formatUserResult(user) {
        if (!user.id)
            return user.text;
        const text = user.text;
        const fullName = text;
        const department = user.departmentName;
        return $(`<span>${fullName}</span><div><small style="color: #a5a0a0">${l('DepartmentName')}: ${department}</small></div>`);
    }

    function formatUserSelection(user) {
        if (!user.id)
            return user.text;
        const text = user.text;
        return $(`<span>${text}</span>`);
    }

    function formatGroupSelection(group) {
        if (!group.id)
            return group.text;
        const text = group.text;
        return $(`<span>${text}</span>`);
    }

    function formatGroupResult(group) {
        if (!group.id)
            return group.text;
        const text = group.text;
        return $(`<span>${text}</span><div><small style="color: #a5a0a0">Email: ${group.email}</small></div>`);
    }

    function getCompanies() {
        return _companyAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                companies = data.items.map((company) => {
                    return { id: company.code, text: company.name };
                });
            }));
    }

    function getDepartments() {
        return _departmentAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                departments = data.items.map((department) => {
                    return { id: department.code, text: department.name, email: department.emailAddress };
                });
            }));
    }

    function getDocumentTypes() {
        return _documentTypeAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                documentTypes = data.items.map((documentType) => {
                    return { id: documentType.code, text: documentType.name };
                });
            }));
    }

    function getModules() {
        return _moduleAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                modules = data.items.map((module) => {
                    return { id: module.code, text: module.name };
                });
            }));
    }

    function getDocuments() {
        return _documentAppService.getList({ filter: '', page: 1, size: 10000 })
            .then((data => {
                documents = data.items.map((document) => {
                    return { id: document.code, text: document.name };
                });
                getRelatedDocuments(data);
            }));
    }

    function getRelatedDocuments(documents) {
        if (documentModel.relateToDocuments) {
            const documentCodes = documentModel.relateToDocuments.split(';');
            if (documentCodes && documentCodes.length > 0) {
                for (const code of documentCodes) {
                    const document = documents.items.find(d => d.code === code);
                    if (document) {
                        relatedDocuments.push(document);
                    }
                };
            }
        }
    }

    $(function () {

        let status = 'Hiệu lực: Vẫn còn hiệu lực';
        if (documentModel.replaceByDocuments) {
            if (replaceBy.length > 0) {
                let replaceByTexts = replaceByDocuments.map(d => { return d.name; }).join(';');
                status = `Tình trạng: Đã hết hiệu lực <br> Được thay thế bởi: ${replaceByTexts}`;
            }
        }
        if (moment() > documentModel.reviewDate) {
            status = 'Tình trạng: Đã hết hiệu lực';
        }
        $('#WarningInfo').html(`Tên tài liệu: ${documentModel.name} <br/>Ban hành: ${moment().to(documentModel.effectiveDate)} <br> Hiệu lực còn: ${moment().to(documentModel.reviewDate)} <br> ${status}`);

        const initGrdRelateToDocuments = function () {
            grdRelateToDocuments = new Tabulator("#grdRelateToDocuments", {
                height: "311px",
                layout: "fitColumns",
                data: relatedDocuments,
                selectable: 1,
                movableRows: false,
                resizableRows: true, //allow row order to be changed
                tooltips: true,
                history: true,
                index: "id",
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
                        width: 200
                    },
                    {
                        title: 'Files',
                        field: "fileName",
                        hozAlign: "left",
                        width: 300,
                        formatter: function (cell, formatterParams, onRendered) {
                            //cell - the cell component
                            //formatterParams - parameters set for the column
                            //onRendered - function to call when the formatter has been rendered
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
                                                html = html + `<a target="_blank" href="/api/document-management/filemanager/view-file?sourceDoc=${filePath}"> <i class="${fileIcon}"></i> ${fileNames[index]}</a><br>`;
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

        Promise.all([getDocuments()]).then(() => {
            initGrdRelateToDocuments();
        });

    });

    $(function () {
        const initGrdAppendixDocuments = function () {

            grdAppendixDocuments = new Tabulator("#grdAppendixDocuments", {
                height: "311px",
                layout: "fitColumns",
                selectable: 1,
                data: documentModel.appendixes,
                history: true,
                columns: [
                    {
                        title: 'Stt',
                        formatter: "rownum",
                        hozAlign: "center",
                        width: 80
                    },
                    {
                        title: "Name",
                        field: "name",
                        hozAlign: "left"
                    },
                    {
                        title: 'File',
                        field: "fileName",
                        hozAlign: "left",
                        width: 300,
                        formatter: function (cell, formatterParams, onRendered) {
                            //cell - the cell component
                            //formatterParams - parameters set for the column
                            //onRendered - function to call when the formatter has been rendered
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
                                                html = html + `<a target="_blank" href="/api/document-management/filemanager/view-file?sourceDoc=${filePath}"> <i class="${fileIcon}"></i> ${fileNames[index]}</a><br>`;
                                            }
                                        }
                                    }
                                }
                            }
                            return html;
                        }
                    },
                    {
                        title: "Số xoán xét",
                        field: "reviewNumber",
                        validator: "required",
                        hozAlign: "left",
                        width: 200
                    }
                ],
            });
        }

        initGrdAppendixDocuments();
    });

    $(function () {

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
            data: groups,
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
            getDocuments()
        ]).then(() => {

            $Approver.select2({
                data: users,
                templateResult: formatUserResult,
                templateSelection: formatUserSelection
            });

            let approver = documentModel.approver;
            $Approver.val(approver.split(';')).trigger('change');

            $Drafter.select2({
                data: users,
                templateResult: formatUserResult,
                templateSelection: formatUserSelection
            });

            let drafter = documentModel.drafter;

            $Drafter.val(drafter.split(';')).trigger('change');

            $Auditor.select2({
                data: users,
                templateResult: formatUserResult,
                templateSelection: formatUserSelection
            });

            let auditor = documentModel.auditor;
            $Auditor.val(auditor.split(';')).trigger('change');

            $CompanyCode.select2({
                data: companies
            });

            $CompanyCode.val([documentModel.companyCode]).trigger('change');

            $DepartmentCode.select2({
                data: departments
            });

            $DepartmentCode.val([documentModel.departmentCode]).trigger('change');

            $DocumentType.select2({
                data: documentTypes
            });
            $DocumentType.val([documentModel.documentType]).trigger('change');

            $Module.select2({
                data: modules
            });
            $Module.val([documentModel.module]).trigger('change');

            $IssuedToEntire.select2({
                data: departments,
                templateResult: formatGroupResult,
                templateSelection: formatGroupSelection
            });
            $IssuedToEntire.val(documentModel.issuedToEntire.split(';')).trigger('change');
            
            initGrdRelateToDocuments();

            initGrdAppendixDocuments();
            // all requests finished successfully
        }).catch(() => {
            // all requests finished but one or more failed
        })

        // Effective date
        $('#EffectiveDate').datepicker({
            language: abp.localization.currentCulture.cultureName,
            showOn: 'focus',
            autoclose: true
        });

        // Review date
        $('#ReviewDate').datepicker({
            language: abp.localization.currentCulture.cultureName,
            showOn: 'focus',
            autoclose: true
        });

        $('#EffectiveDate').datepicker('setDate', new Date(documentModel.effectiveDate));
        $('#ReviewDate').datepicker('setDate', new Date(documentModel.reviewDate));
    });

})();