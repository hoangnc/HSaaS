(function () {
    const l = abp.localization.getResource('MasterData');
    const _moduleAppService = masterData.modules.module;

    const _editModal = new abp.ModalManager(
        abp.appPath + 'MasterData/Modules/EditModal'
    );

    const _createModal = new abp.ModalManager(
        abp.appPath + 'MasterData/Modules/CreateModal'
    );

    let _dataTable = null;

    abp.ui.extensions.entityActions.get('masterData.module').addContributor(
        function(actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted(
                            'MasterData.Modules.Update'
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
                            'MasterData.Modules.Delete'
                        ),
                        confirmMessage: function (data) {
                            return l(
                                'ModuleDeletionConfirmationMessage',
                                data.record.name
                            );
                        },
                        action: function (data) {
                            _moduleAppService
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

    abp.ui.extensions.tableColumns.get('masterData.module').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('masterData.module').actions.toArray()
                        }
                    },
                    {
                        title: l("Display:ModuleCode"),
                        data: 'code'
                    },
                    {
                        title: l("Display:ModuleName"),
                        data: 'name',
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );

    $(function () {
        const _$wrapper = $('#ModulesWrapper');

        _dataTable = _$wrapper.find('table').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                paging: true,
                scrollX: true,
                serverSide: true,
                ajax: abp.libs.datatables.createAjax(_moduleAppService.getList),
                columnDefs: abp.ui.extensions.tableColumns.get('masterData.module').columns.toArray(),
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _$wrapper.find('button[name=CreateModule]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})();
