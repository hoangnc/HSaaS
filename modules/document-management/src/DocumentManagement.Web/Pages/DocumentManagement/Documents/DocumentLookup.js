abp.modals.DocumentLookup = function () {
    function getFileExtension(filename) {
        const ext = /^.+\.([^.]+)$/.exec(filename);
        return ext == null ? "" : ext[1];
    }

    let _modalManager;

    function initModal(modalManager, args) {
        _modalManager = modalManager;
    };

    const l = abp.localization.getResource('DocumentManagement');
    const _documentAppService = documentManagement.document.document;
    const _companyAppService = masterData.companies.company;
    const _departmentAppService = masterData.departments.department;
    const _documentTypeAppService = masterData.documentTypes.documentType;
    const _moduleAppService = masterData.modules.module;

    const updateDocumentUrl = '';

    let companies = [];
    let departments = [];
    let documenTypes = [];
    let modules = [];

    function getModules() {
        return _moduleAppService.getList({ filter: '', skipCount: 0, maxResultCount: 100 })
            .then((data => {
                modules = data.items.map((module) => {
                    return { id: module.code, text: module.name };
                });
            }));
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
                    return { id: department.code, text: department.name };
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

    const transformDocuments = async function (data) {
        let result = {
            data: [],
            total: 0
        };

        await Promise.all([
            getCompanies(),
            getDepartments(),
            getDocumentTypes(),
            getModules()
        ]).then(() => {
            let documents = data.items;

            result = {
                data: documents.map(document => {
                    const company = companies.find(c => c.id === document.companyCode);
                    if (company) {
                        document.companyName = company.text;
                    }

                    const department = departments.find(d => d.id === document.departmentCode);

                    if (department) {
                        document.departmentName = department.text;
                    }

                    const documentType = documentTypes.find(d => d.id === document.documentType);
                    if (documentType) {
                        document.documentType = documentType.text;
                    }

                    const module = modules.find(d => d.id === document.module);
                    if (module) {
                        document.module = module.text;
                    }

                    return document;
                }),
                total: data.totalCount
            };
        });

        return result;
    };

    $(function () {
        const txtSearch = $('#txtSearch');
        let grdDocument;
        txtSearch.on("keyup", function (e) {
            const key = event.charCode ? event.charCode : event.keyCode ? event.keyCode : 0;
            if (key === 13) {
                grdDocument.replaceData();
            }
        });

        let documents = [];

        $("#btnSelect").click(e => {
            const documentSelected = grdDocument.getSelectedData();
            _modalManager.setResult(documentSelected);
        });


        $(document).ready(function () {

            const _$wrapper = $('#DocumentLookupsWrapper');
            //Build Tabulator
            grdDocument = new Tabulator("#grdDocument", {
                height: "450px",
                layout: "fitColumns",
                selectable: 1,
                resizableRows: true,       //allow row order to be changed
                tooltips: true,
                ajaxSorting: false,
                ajaxLoader: true,
                ajaxURL: '/api/document-management/documents/get-list',
                ajaxProgressiveLoad: "scroll",
                ajaxURLGenerator: function (url, config, params) {
                    //url - the url from the ajaxURL property or setData function
                    //config - the request config object from the ajaxConfig property
                    //params - the params object from the ajaxParams property, this will also include any pagination, filter and sorting properties based on table setup

                    //return request url
                    let urlParameters = Object.entries(params).map(e => e.join('=')).join('&');

                    return url + '?' + urlParameters + `&token=${txtSearch.val()}&advancedSearch=${$('#AdvancedSearch').is(':checked')}`;//+ "?params=" + encodeURI(JSON.stringify(params)); //encode parameters as a json object
                },
                ajaxResponse: function (url, params, response) {
                    //url - the URL of the request
                    //params - the parameters passed with the request
                    //response - the JSON object returned in the body of the response.

                    return transformDocuments(response);
                    // return { data: response.items, total: response.totalCount }; //return the tableData property of a response json object
                },
                rowDblClick: function (e, row) {
                    //e - the click event object
                    //row - row component
                    const documentSelected = row.getData();
                    console.log(documentSelected);
                    _modalManager.setResult([documentSelected]);
                    _modalManager.close();
                    //$("#btnSelect").click();
                },
                paginationSize: 50,
                placeholder: "No Data Set",
                history: true,
                columns: [
                    /*{
                        formatter: "rowSelection", titleFormatter: "rowSelection", hozAlign: "center", headerSort: false, cellClick: function (e, cell) {
                            cell.getRow().toggleSelect();
                        }
                    },*/
                    {
                        title: '',
                        formatter: "rownum",
                        hozAlign: "center",
                        width: 80
                    },
                    {
                        title: l("Code"),
                        field: "code",
                        hozAlign: "left",
                        width: 200,
                        headerFilter: "input"
                    },
                    {
                        title: 'Files', field: "fileName", hozAlign: "left", width: 300, formatter: function (cell, formatterParams, onRendered) {
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
                                                html = html + `<a target="_blank" href="/downloadfile/view-file?sourceDoc=${filePath}"> <i class="${fileIcon}"></i> ${fileNames[index]}</a><br>`;
                                            }
                                        }
                                    }
                                }
                            }
                            return html;
                        }
                    },
                    { title: l("Name"), field: 'name', hozAlign: "left", width: 200, headerFilter: "input" },
                    { title: l("CompanyName"), field: 'companyName', hozAlign: "left", width: 200 },
                    { title: l("DepartmentName"), field: 'departmentName', hozAlign: "left", width: 200 },
                    { title: l("Module"), field: 'module', hozAlign: "left", width: 200 },
                    {
                        title: l("ReplaceOf"), field: "replaceOfName", width: 200, hozAlign: "left", formatter: function (cell, formatterParams, onRendered) {
                            //cell - the cell component
                            //formatterParams - parameters set for the column
                            //onRendered - function to call when the formatter has been rendered
                            let html = '';
                            if (cell.getValue()) {
                                const documents = cell.getValue().split(';');

                                if (documents) {
                                    if (documents.length > 0) {
                                        const documentCodes = cell.getRow().getData().replaceOf.split(';');

                                        for (let index = 0; index < documents.length; index++) {
                                            if (!documents[index]) {
                                                html = html + `<a target="_blank" href="/document/detail?code=${documentCodes[index]}"> <i class="fa fa-file-o"></i> ${documents[index]}</a><br>`;
                                            }
                                        }
                                    }
                                }
                            }
                            return html;
                        }
                    },
                    {
                        title: l("RelateToDocuments"), field: "relateToDocumentNames", width: 200, hozAlign: "left", formatter: function (cell, formatterParams, onRendered) {
                            let html = '';
                            if (cell.getValue()) {
                                const documents = cell.getValue().split(';');

                                if (documents) {

                                    if (documents.length > 0) {
                                        const documentCodes = cell.getRow().getData().relateToDocuments.split(';');

                                        for (let index = 0; index < documents.length; index++) {
                                            if (documents[index]) {
                                                html = html + `<a target="_blank" href="/document/detail?code=${documentCodes[index]}"> <i class="fa fa-file-o"></i> ${documents[index]}</a><br>`;
                                            }
                                        }

                                    }
                                }
                            }
                            return html;
                        }
                    },
                    { title: l("DocumentType"), field: 'documentType', hozAlign: "left", width: 200 },
                    { title: l("DocumentNumber"), field: 'documentNumber', hozAlign: "left", width: 200 },
                    { title: l("ReviewNumber"), field: 'reviewNumber', hozAlign: "left", width: 200 },
                ],
            });

        });
    });

    return {
        initModal: initModal
    };
};
