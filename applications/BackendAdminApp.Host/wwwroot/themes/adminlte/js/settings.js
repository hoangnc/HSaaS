var abp = abp || {};

(function ($) {
    'use strict'
    abp.theme = abp.theme || {
        adminlte: {}};
   
    abp.theme.adminlte.key = 'abp.theme.adminlte.settings';

    abp.theme.adminlte.settings = {
        no_border: true,
        text_sm_body: true,
        text_sm_header: true,
        text_sm_sidebar: true,
        text_sm_footer: true,
        flat_sidebar: true,
        legacy_sidebar: true,
        compact_sidebar: true,
        child_indent_sidebar: true,
        no_expand_sidebar: true,
        text_sm_brand: true,
        navbar_color: '',
        accent_color: '',
        dark_sidebar_color: '',
        light_sidebar_color: '',
        brand_logo_color: '',
    };

    abp.theme.adminlte.removeSettingsIfExist = function() {
        if (localStorage.getItem(abp.theme.adminlte.key)) {
            localStorage.removeItem(abp.theme.adminlte.key);
        }
    }

    abp.theme.adminlte.setSetting = function (setting, value) {
        abp.theme.adminlte.getSettings();
        abp.theme.adminlte.settings[setting] = value;
        localStorage.setItem(abp.theme.adminlte.key, JSON.stringify(abp.theme.adminlte.settings));
    }

    abp.theme.adminlte.setSettings = function () {
        abp.theme.adminlte.removeSettingsIfExist();

        localStorage.setItem(abp.theme.adminlte.key, JSON.stringify(abp.theme.adminlte.settings));
    }

    abp.theme.adminlte.getSettings = function () {
        abp.theme.adminlte.settings = JSON.parse(localStorage.getItem(abp.theme.adminlte.key));
    }

    abp.theme.adminlte.setDefaultSettings = function () {
        if (!localStorage.getItem(abp.theme.adminlte.key)) {
            abp.theme.adminlte.setSettings();
        }

        abp.theme.adminlte.getSettings();
        var settings = abp.theme.adminlte.settings;

        abp.theme.adminlte.setNoBorder(settings.no_border);
        abp.theme.adminlte.setTextSmBody(settings.text_sm_body);
        abp.theme.adminlte.setTextSmHeader(settings.text_sm_header);
        abp.theme.adminlte.setTextSmFooter(settings.text_sm_footer);
        abp.theme.adminlte.setFlatSidebar(settings.flat_sidebar);
        abp.theme.adminlte.setTextSmSidebar(settings.text_sm_sidebar);
        abp.theme.adminlte.setLegacySidebar(settings.legacy_sidebar);
        abp.theme.adminlte.setCompactSidebar(settings.compact_sidebar);
        abp.theme.adminlte.setChildIndentSidebar(settings.child_indent_sidebar);
        abp.theme.adminlte.setNoExpandSidebar(settings.no_expand_sidebar);
        abp.theme.adminlte.setTextSmBrand(settings.text_sm_brand);
    }

    abp.theme.adminlte.setNoBorder = function (value) {
        setSetting($('.main-header'),
            'border-bottom-0',
            value,
            'no_border'
        );
    }

    abp.theme.adminlte.setTextSmBody = function (value) {
        setSetting($('body'),
            'text-sm',
            value,
            'text_sm_body'
        );
    }

    abp.theme.adminlte.setTextSmHeader = function (value) {
        setSetting($('.main-header'),
            'text-sm',
            value,
            'text_sm_header'
        );
    }

    abp.theme.adminlte.setTextSmSidebar = function (value) {
        setSetting($('.nav-sidebar'),
            'text-sm',
            value,
            'text_sm_sidebar'
        );
    }

    abp.theme.adminlte.setTextSmFooter = function (value) {
        setSetting($('.main-footer'),
            'text-sm',
            value,
            'text_sm_footer'
        );
    }

    abp.theme.adminlte.setFlatSidebar = function (value) {
        setSetting($('.nav-sidebar'),
            'nav-flat',
            value,
            'flat_sidebar'
        );
    }

    abp.theme.adminlte.setLegacySidebar = function (value) {
        setSetting($('.nav-sidebar'),
            'nav-legacy',
            value,
            'legacy_sidebar'
        );
    }

    abp.theme.adminlte.setCompactSidebar = function (value) {
        setSetting($('.nav-sidebar'),
            'nav-compact',
            value,
            'compact_sidebar'
        );
    }

    abp.theme.adminlte.setChildIndentSidebar = function (value) {
        setSetting($('.nav-sidebar'),
            'nav-child-indent',
            value,
            'child_indent_sidebar'
        );
    }

    abp.theme.adminlte.setNoExpandSidebar = function (value) {
        setSetting($('.main-sidebar'),
            'sidebar-no-expand',
            value,
            'no_expand_sidebar'
        );
    }

    abp.theme.adminlte.setTextSmBrand = function (value) {
        setSetting($('.brand-link'),
            'text-sm',
            value,
            'text_sm_brand'
        );
    }

    function setSetting($el, className, value, setting) {
        if (value) {
            $el.addClass(className);
        }
        else {
            $el.removeClass(className);
        }
        abp.theme.adminlte.setSetting(setting, value);
    }
    
})(jQuery);


