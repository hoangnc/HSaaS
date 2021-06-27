/**
 * AdminLTE Demo Menu
 * ------------------
 * You should not use this file in production.
 * This file is for demo purposes only.
 */
(function ($) {
    'use strict'

    $.busyLoadSetup({ background: "rgba(100, 100, 0, 0.86)" });

    $(document).ajaxStart(function () {
        $.busyLoadFull("show", {
            spinner: "cubes"
        });
    });

    $(document).ajaxStop(function () {
        $.busyLoadFull("hide");
    });

    $(document).ajaxSuccess(function (event, xhr, settings) {
        let message = 'Successfully!';
        if (xhr.message !== undefined) {
            message = xhr.message.infor;
            const title = 'Announce';
            $.toast({
                heading: title,
                text: message,
                position: 'top-right',
                icon: 'info',
                loader: true,        // Change it to false to disable loader
                loaderBg: '#9EC600',  // To change the background,
                hideAfter: 1500,
                afterHidden: function () {
                    if (xhr.message.returnUrl !== undefined) {
                        window.location.href = xhr.message.returnUrl;
                    }
                }
            })
        }
    });

    var $sidebar = $('.control-sidebar')
    var $container = $('<div />', {
        class: 'p-3 control-sidebar-content'
    })

    // Set default settings admin lte
    abp.theme.adminlte.setDefaultSettings();

    $sidebar.append($container)

    var navbar_dark_skins = [
        'navbar-primary',
        'navbar-secondary',
        'navbar-info',
        'navbar-success',
        'navbar-danger',
        'navbar-indigo',
        'navbar-purple',
        'navbar-pink',
        'navbar-teal',
        'navbar-cyan',
        'navbar-dark',
        'navbar-gray-dark',
        'navbar-gray',
    ]

    var navbar_light_skins = [
        'navbar-light',
        'navbar-warning',
        'navbar-white',
        'navbar-orange',
    ]

    $container.append(
        '<h5>Customize AdminLTE</h5><hr class="mb-2"/>'
    )

    var $no_border_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.no_border || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setNoBorder($(this).is(':checked'));
    })
    var $no_border_container = $('<div />', { 'class': 'mb-1' }).append($no_border_checkbox).append('<span>No Navbar border</span>')
    $container.append($no_border_container)

    var $text_sm_body_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.text_sm_body || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setTextSmBody($(this).is(':checked'));
    })
    var $text_sm_body_container = $('<div />', { 'class': 'mb-1' }).append($text_sm_body_checkbox).append('<span>Body small text</span>')
    $container.append($text_sm_body_container)

    var $text_sm_header_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.text_sm_header || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setTextSmHeader($(this).is(':checked'));
    })
    var $text_sm_header_container = $('<div />', { 'class': 'mb-1' }).append($text_sm_header_checkbox).append('<span>Navbar small text</span>')
    $container.append($text_sm_header_container)

    var $text_sm_sidebar_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.text_sm_sidebar || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setTextSmSidebar($(this).is(':checked'));
    })
    var $text_sm_sidebar_container = $('<div />', { 'class': 'mb-1' }).append($text_sm_sidebar_checkbox).append('<span>Sidebar nav small text</span>')
    $container.append($text_sm_sidebar_container)

    var $text_sm_footer_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.text_sm_footer || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setTextSmFooter($(this).is(':checked'));
    })
    var $text_sm_footer_container = $('<div />', { 'class': 'mb-1' }).append($text_sm_footer_checkbox).append('<span>Footer small text</span>')
    $container.append($text_sm_footer_container)

    var $flat_sidebar_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.flat_sidebar || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setFlatSidebar($(this).is(':checked'));
    })
    var $flat_sidebar_container = $('<div />', { 'class': 'mb-1' }).append($flat_sidebar_checkbox).append('<span>Sidebar nav flat style</span>')
    $container.append($flat_sidebar_container)

    var $legacy_sidebar_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.legacy_sidebar || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setLegacySidebar($(this).is(':checked'));
    })

    var $legacy_sidebar_container = $('<div />', { 'class': 'mb-1' }).append($legacy_sidebar_checkbox).append('<span>Sidebar nav legacy style</span>')
    $container.append($legacy_sidebar_container)

    var $compact_sidebar_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.compact_sidebar || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setCompactSidebar($(this).is(':checked'));
    })
    var $compact_sidebar_container = $('<div />', { 'class': 'mb-1' }).append($compact_sidebar_checkbox).append('<span>Sidebar nav compact</span>')
    $container.append($compact_sidebar_container)

    var $child_indent_sidebar_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.child_indent_sidebar || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setChildIndentSidebar($(this).is(':checked'))
    })
    var $child_indent_sidebar_container = $('<div />', { 'class': 'mb-1' }).append($child_indent_sidebar_checkbox).append('<span>Sidebar nav child indent</span>')
    $container.append($child_indent_sidebar_container)

    var $no_expand_sidebar_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.no_expand_sidebar || false,
        'class': 'mr-1'
    }).on('click', function () {
        abp.theme.adminlte.setNoExpandSidebar($(this).is(':checked'))
    })
    var $no_expand_sidebar_container = $('<div />', { 'class': 'mb-1' }).append($no_expand_sidebar_checkbox).append('<span>Main Sidebar disable hover/focus auto expand</span>')
    $container.append($no_expand_sidebar_container)

    var $text_sm_brand_checkbox = $('<input />', {
        type: 'checkbox',
        value: 1,
        checked: abp.theme.adminlte.settings.text_sm_brand|| false,
        'class': 'mr-1'
    }).on('click', function () {
        if ($(this).is(':checked')) {
            $('.brand-link').addClass('text-sm')
        } else {
            $('.brand-link').removeClass('text-sm')
        }
        abp.theme.adminlte.setTextSmBrand($(this).is(':checked'));
    })
    var $text_sm_brand_container = $('<div />', { 'class': 'mb-4' }).append($text_sm_brand_checkbox).append('<span>Brand small text</span>')
    $container.append($text_sm_brand_container)

    $container.append('<h6>Navbar Variants</h6>')

    var $navbar_variants = $('<div />', {
        'class': 'd-flex'
    })
    var navbar_all_colors = navbar_dark_skins.concat(navbar_light_skins)
    var $navbar_variants_colors = createSkinBlock(navbar_all_colors, function (e) {
        var color = $(this).data('color')
        var $main_header = $('.main-header')
        $main_header.removeClass('navbar-dark').removeClass('navbar-light')
        navbar_all_colors.map(function (color) {
            $main_header.removeClass(color)
        })

        if (navbar_dark_skins.indexOf(color) > -1) {
            $main_header.addClass('navbar-dark')
        } else {
            $main_header.addClass('navbar-light')
        }

        $main_header.addClass(color)
    })

    $navbar_variants.append($navbar_variants_colors)

    $container.append($navbar_variants)

    var sidebar_colors = [
        'bg-primary',
        'bg-warning',
        'bg-info',
        'bg-danger',
        'bg-success',
        'bg-indigo',
        'bg-navy',
        'bg-purple',
        'bg-fuchsia',
        'bg-pink',
        'bg-maroon',
        'bg-orange',
        'bg-lime',
        'bg-teal',
        'bg-olive'
    ]

    var accent_colors = [
        'accent-primary',
        'accent-warning',
        'accent-info',
        'accent-danger',
        'accent-success',
        'accent-indigo',
        'accent-navy',
        'accent-purple',
        'accent-fuchsia',
        'accent-pink',
        'accent-maroon',
        'accent-orange',
        'accent-lime',
        'accent-teal',
        'accent-olive'
    ]

    var sidebar_skins = [
        'sidebar-dark-primary',
        'sidebar-dark-warning',
        'sidebar-dark-info',
        'sidebar-dark-danger',
        'sidebar-dark-success',
        'sidebar-dark-indigo',
        'sidebar-dark-navy',
        'sidebar-dark-purple',
        'sidebar-dark-fuchsia',
        'sidebar-dark-pink',
        'sidebar-dark-maroon',
        'sidebar-dark-orange',
        'sidebar-dark-lime',
        'sidebar-dark-teal',
        'sidebar-dark-olive',
        'sidebar-light-primary',
        'sidebar-light-warning',
        'sidebar-light-info',
        'sidebar-light-danger',
        'sidebar-light-success',
        'sidebar-light-indigo',
        'sidebar-light-navy',
        'sidebar-light-purple',
        'sidebar-light-fuchsia',
        'sidebar-light-pink',
        'sidebar-light-maroon',
        'sidebar-light-orange',
        'sidebar-light-lime',
        'sidebar-light-teal',
        'sidebar-light-olive'
    ]

    $container.append('<h6>Accent Color Variants</h6>')
    var $accent_variants = $('<div />', {
        'class': 'd-flex'
    })
    $container.append($accent_variants)
    $container.append(createSkinBlock(accent_colors, function () {
        var color = $(this).data('color')
        var accent_class = color
        var $body = $('body')
        accent_colors.map(function (skin) {
            $body.removeClass(skin)
        })

        $body.addClass(accent_class)
    }))

    $container.append('<h6>Dark Sidebar Variants</h6>')
    var $sidebar_variants = $('<div />', {
        'class': 'd-flex'
    })
    $container.append($sidebar_variants)
    $container.append(createSkinBlock(sidebar_colors, function () {
        var color = $(this).data('color')
        var sidebar_class = 'sidebar-dark-' + color.replace('bg-', '')
        var $sidebar = $('.main-sidebar')
        sidebar_skins.map(function (skin) {
            $sidebar.removeClass(skin)
        })

        $sidebar.addClass(sidebar_class)
    }))

    $container.append('<h6>Light Sidebar Variants</h6>')
    var $sidebar_variants = $('<div />', {
        'class': 'd-flex'
    })
    $container.append($sidebar_variants)
    $container.append(createSkinBlock(sidebar_colors, function () {
        var color = $(this).data('color')
        var sidebar_class = 'sidebar-light-' + color.replace('bg-', '')
        var $sidebar = $('.main-sidebar')
        sidebar_skins.map(function (skin) {
            $sidebar.removeClass(skin)
        })

        $sidebar.addClass(sidebar_class)
    }))

    var logo_skins = navbar_all_colors
    $container.append('<h6>Brand Logo Variants</h6>')
    var $logo_variants = $('<div />', {
        'class': 'd-flex'
    })
    $container.append($logo_variants)
    var $clear_btn = $('<a />', {
        href: 'javascript:void(0)'
    }).text('clear').on('click', function () {
        var $logo = $('.brand-link')
        logo_skins.map(function (skin) {
            $logo.removeClass(skin)
        })
    })
    $container.append(createSkinBlock(logo_skins, function () {
        var color = $(this).data('color')
        var $logo = $('.brand-link')
        logo_skins.map(function (skin) {
            $logo.removeClass(skin)
        })
        $logo.addClass(color)
    }).append($clear_btn))

    function createSkinBlock(colors, callback) {
        var $block = $('<div />', {
            'class': 'd-flex flex-wrap mb-3'
        })

        colors.map(function (color) {
            var $color = $('<div />', {
                'class': (typeof color === 'object' ? color.join(' ') : color).replace('navbar-', 'bg-').replace('accent-', 'bg-') + ' elevation-2'
            })

            $block.append($color)

            $color.data('color', color)

            $color.css({
                width: '40px',
                height: '20px',
                borderRadius: '25px',
                marginRight: 10,
                marginBottom: 10,
                opacity: 0.8,
                cursor: 'pointer'
            })

            $color.hover(function () {
                $(this).css({ opacity: 1 }).removeClass('elevation-2').addClass('elevation-4')
            }, function () {
                $(this).css({ opacity: 0.8 }).removeClass('elevation-4').addClass('elevation-2')
            })

            if (callback) {
                $color.on('click', callback)
            }
        })

        return $block
    }
})(jQuery)
