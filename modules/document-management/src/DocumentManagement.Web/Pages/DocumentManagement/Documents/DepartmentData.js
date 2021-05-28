const datePattern = abp.localization.currentCulture.dateTimeFormat.shortDatePattern;
const isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function () {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i) || navigator.userAgent.match(/WPDesktop/i);
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};

function getFrozen() {
    return isMobile.any() ? false : true;
} 
function getFileExtension(filename) {
    const ext = /^.+\.([^.]+)$/.exec(filename);
    return ext == null ? "" : ext[1];
}

(function () {

    const l = abp.localization.getResource('DocumentManagement');
    const _documentAppService = documentManagement.document.document;
    const _companyAppService = masterData.companies.company;
    const _departmentAppService = masterData.departments.department;
    const _documentTypeAppService = masterData.documentTypes.documentType;
    const _moduleAppService = masterData.modules.module;

    const updateDocumentUrl = '/DocumentManagement/Documents/Edit';
    const detailDocumentUrl = '/DocumentManagement/Documents/Detail';

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

                    if (document.relateToDocuments) {

                        let relateToDocuments = document.relateToDocuments
                            .split(';')
                            .map(d => {
                                const relateToDocument = documents.find(d1 => d1.code === d);
                                if (relateToDocument) {
                                    return relateToDocument.name;
                                }
                                return d;
                            });

                        document.relateToDocumentNames = relateToDocuments.join(';');
                    } else {
                        document.relateToDocumentNames = '';
                    }

                    if (document.replaceFor) {

                        const replaceForDocuments = document.replaceFor
                            .split(';')
                            .map(d => {
                                const replaceForDocument = documents.find(d1 => d1.code === d);
                                if (replaceForDocument) {
                                    return replaceForDocument.name;
                                }
                                return d;
                            });

                        document.replaceForName = replaceForDocuments.join(';');
                    } else {
                        document.replaceForName = '';
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
        let grdDocument = $("#grdDocument");
        txtSearch.on("keyup", function (e) {
            const key = event.charCode ? event.charCode : event.keyCode ? event.keyCode : 0;
            if (key === 13) {
                grdDocument.replaceData();
            }
        });

        let documents = [];

        function initGrdDocument() {
            //Build Tabulator
            grdDocument = new Tabulator("#grdDocument", {
                height: "450px",
                layout: "fitColumns",
                pagination: "remote",
                resizableRows: true,       //allow row order to be changed
                tooltips: true,
                ajaxSorting: false,
                ajaxLoader: true,
                ajaxURL: '/api/document-management/documents/filtered-paged-list-by-department-code',
                ajaxProgressiveLoad: "scroll",
                ajaxURLGenerator: function (url, config, params) {

                    let urlParameters = Object.entries(params).map(e => e.join('=')).join('&');

                    return url + '?' + urlParameters + `&filter=${txtSearch.val()}&departmentCode=${departmentCode}&advancedSearch=${$('#AdvancedSearch').is(':checked')}`;//+ "?params=" + encodeURI(JSON.stringify(params)); //encode parameters as a json object
                },
                ajaxResponse: function (url, params, response) {

                    return transformDocuments(response);
                },
                paginationSize: 50,
                placeholder: "Không có dữ liệu",
                history: true,
                columns: [
                    {
                        title: 'Stt',
                        formatter: "rownum",
                        hozAlign: "center",
                        width: 80,
                        frozen: getFrozen()
                    },
                    {
                        title: l("Code"),
                        field: "code",
                        hozAlign: "left",
                        frozen: getFrozen(),
                        width: 200,
                        formatter: function (cell, formatterParams, onRendered) {
                            //cell - the cell component
                            //formatterParams - parameters set for the column
                            //onRendered - function to call when the formatter has been rendered
                            if (abp.auth.isGranted(
                                'DocumentManagement.Documents.Update'
                            )) {
                                return `<a href="${updateDocumentUrl}?id=${cell.getRow().getData().id}"> <i class="fa fa-pencil"></i> ${cell.getValue()} </a>`; //return the contents of the cell;
                            }
                            else {
                                return `<a href="${detailDocumentUrl}?code=${cell.getRow().getData().code}"> <i class="fa fa-info"></i> ${cell.getValue()} </a>`; //return the contents of the cell;
                            }
                        }
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
                        title: l("Appendix"),
                        field: "",
                        width: 200,
                        hozAlign: "left",
                        formatter: function (cell, formatterParams, onRendered) {
                            //cell - the cell component
                            //formatterParams - parameters set for the column
                            //onRendered - function to call when the formatter has been rendered
                            let html = '';
                            const document = cell.getData();
                            if (document) {
                                const appendixes = document.appendixes;

                                if (appendixes) {
                                    if (appendixes.length > 0) {
                                        for (let index = 0; index < appendixes.length; index++) {
                                            if (appendixes[index]) {
                                                // html = html + `<a target="_blank" href="/appendix/detail?code=${appendixes[index].code}"> <i class="fa fa-file-o"></i> ${appendixes[index].name}</a><br>`;
                                                const filePath = `${appendixes[index].folderName}/${appendixes[index].fileName}`;
                                                let fileIcon = 'fa fa-file-o';
                                                switch (getFileExtension(appendixes[index].fileName)) {
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
                                                html = html + `<a target="_blank" href="/api/document-management/filemanager/view-file?sourceDoc=${filePath}"> <i class="${fileIcon}"></i> ${appendixes[index].fileName}</a><br>`;
                                            }
                                        }
                                    }
                                }
                            }
                            return html;
                        }
                    },
                    {
                        title: l("Name"),
                        field: 'name',
                        hozAlign: "left",
                        width: 200
                    },
                    {
                        title: l("EffectiveDate"),
                        field: 'effectiveDate',
                        hozAlign: "left",
                        width: 200,
                        formatter: "datetime",
                        formatterParams: {
                            outputFormat: datePattern.toUpperCase(),
                            invalidPlaceholder: "",
                        }
                    },
                    {
                        title: l("ReviewDate"),
                        field: 'reviewDate',
                        hozAlign: "left",
                        width: 200,
                        formatter: "datetime",
                        formatterParams: {
                            outputFormat: datePattern.toUpperCase(),
                            invalidPlaceholder: "",
                        }
                    },
                    {
                        title: l("CompanyName"),
                        field: 'companyName',
                        hozAlign: "left", width: 200
                    },
                    {
                        title: l("DepartmentName"),
                        field: 'departmentName',
                        hozAlign: "left",
                        width: 200
                    },
                    {
                        title: l("Module"),
                        field: 'module',
                        hozAlign: "left",
                        width: 200
                    },
                    {
                        title: l("ReplaceFor"),
                        field: "replaceForName",
                        width: 200,
                        hozAlign: "left",
                        formatter: function (cell, formatterParams, onRendered) {
                            let html = '';

                            if (cell.getValue()) {
                                const replaceForDocuments = cell.getValue().split(';');

                                if (replaceForDocuments) {
                                    if (replaceForDocuments.length > 0) {
                                        const documentCodes = cell.getRow().getData().replaceFor.split(';');

                                        for (let index = 0; index < replaceForDocuments.length; index++) {
                                            if (replaceForDocuments[index]) {
                                                const replaceForDocument = documents.find(d1 => d1.code === replaceForDocuments[index]);
                                                if (replaceForDocument) {
                                                    html = html + `<a target="_blank" href="/documentmanagement/documents/detail?code=${documentCodes[index]}"> <i class="fa fa-file-o"></i> ${replaceForDocument.name}</a><br>`;
                                                } else {
                                                    html = html + `<a target="_blank" href="/documentmanagement/documents/detail?code=${documentCodes[index]}"> <i class="fa fa-file-o"></i> ${replaceForDocuments[index]}</a><br>`;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            return html;
                        }
                    },
                    {
                        title: l("RelateToDocuments"),
                        field: "relateToDocumentNames",
                        width: 200,
                        hozAlign: "left",
                        formatter: function (cell, formatterParams, onRendered) {
                            let html = '';
                            const relateToDocumentNames = cell.getValue();
                            if (relateToDocumentNames) {
                                const documents = relateToDocumentNames.split(';');

                                if (documents) {

                                    if (documents.length > 0) {
                                        const documentCodes = cell.getRow().getData().relateToDocuments.split(';');

                                        for (let index = 0; index < documents.length; index++) {
                                            if (documents[index]) {
                                                html = html + `<a target="_blank" href="/DocumentManagement/documents/detail?code=${documentCodes[index]}"> <i class="fa fa-file-o"></i> ${documents[index]}</a><br>`;
                                            }
                                        }

                                    }
                                }
                            }
                            return html;
                        }
                    },
                    {
                        title: l("DocumentType"),
                        field: 'documentType',
                        hozAlign: "left",
                        width: 200
                    },
                    {
                        title: l("DocumentNumber"),
                        field: 'documentNumber',
                        hozAlign: "left",
                        width: 200
                    },
                    {
                        title: l("ReviewNumber"),
                        field: 'reviewNumber',
                        hozAlign: "left",
                        width: 200
                    },
                ],
            });
        }

        $(document).ready(function () {
            initGrdDocument();

            //trigger download of data.xlsx file
            document.getElementById("download-xlsx").addEventListener("click", function () {
                grdDocument.download("xlsx", "documents.xlsx", { sheetName: "Document" });
            });

            const _$wrapper = $('#IdentityUsersWrapper');

            _$wrapper.find('button[name=CreateDocument]').click(function (e) {
                e.preventDefault();
                _createModal.open();
            });
        });
    });
})();
