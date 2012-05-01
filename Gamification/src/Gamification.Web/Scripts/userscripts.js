/// <reference path="jquery-1.7.2-vsdoc.js" />

(function ($) {

    function toggleBlock(elem) {
        $(elem).find('option').each(function () {
            var $this = $(this);
            var value = $this.val();

            if ($this.is(':selected')) {
                $('div[data-depend-value=' + value + ']').show();
            }
            else {
                $('div[data-depend-value=' + value + ']').hide();
            }
        });
    }

    $.fn.toggleBlocks = function (onToggleFn) {
        function callOnToggle(self) {
            if (onToggleFn != null && typeof (onToggleFn) === 'function') {
                onToggleFn.apply(self);
            }
        }

        return $(this).each(function () {
            if (this.tagName !== 'SELECT')
                return;

            toggleBlock(this);
            callOnToggle(this);

            $(this).change(function () {
                toggleBlock(this);
                callOnToggle(this);
            }); // --change
        }); // --each
    };  // --plugin

    
    

    $.fn.submitDialog = function (title, openDialogElementSelector) {
        var me = this;
        $(me).dialog({
            title: title,
            buttons: {
                'OK': function () {
                    $(this).find('form').submit();
                },
                'Cancel': function () {
                    $(this).find('input[type=text]').val('');
                    $(this).dialog('close');
                }
            }
        });

        $(openDialogElementSelector).click(function () {
            $(me).dialog('open');
        });
    };

})(jQuery);