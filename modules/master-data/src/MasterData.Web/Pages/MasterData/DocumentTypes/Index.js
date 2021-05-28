(function () {
    const l = abp.localization.getResource('MasterData');
    const _documentTypeAppService = masterData.documentTypes.documentType;

    const _editModal = new abp.ModalManager(
        abp.appPath + 'MasterData/DocumentTypes/EditModal'
    );

    const _createModal = new abp.ModalManager(
        abp.appPath + 'MasterData/DocumentTypes/CreateModal'
    );

    let _dataTable = null;

    abp.ui.extensions.entityActions.get('masterData.documentType').addContributor(
        function(actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted(
                            'MasterData.DocumentTypes.Update'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted(
                            'MasterData.DocumentTypes.Delete'
                        ),
                        confirmMessage: function (data) {
                            return l(
                                'DocumentTypeDeletionConfirmationMessage',
                                data.record.name
                            );
                        },
                        action: function (data) {
                            _documentTypeAppService
                                .delete(data.record.id)
                                .then(function () {
                                    _dataTable.ajax.reload();
                                });
                        },
                    }
                ]
            );
        }
    );

    abp.ui.extensions.tableColumns.get('masterData.documentType').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('masterData.documentType').actions.toArray()
                        }
                    },
                    {
                        title: l("Display:DocumentTypeCode"),
                        data: 'code'
                    },
                    {
                        title: l("Display:DocumentTypeName"),
                        data: 'name',
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );

    $(function () {
        const _$wrapper = $('#DocumentTypesWrapper');

        _dataTable = _$wrapper.find('table').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                paging: true,
                scrollX: true,
                serverSide: true,
                ajax: abp.libs.datatables.createAjax(_documentTypeAppService.getList),
                columnDefs: abp.ui.extensions.tableColumns.get('masterData.documentType').columns.toArray(),
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _$wrapper.find('button[name=CreateDocumentType]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})();
