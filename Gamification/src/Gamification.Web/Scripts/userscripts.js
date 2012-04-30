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

    $.fn.toggleBlocks = function () {
        return $(this).each(function () {
            if (this.tagName !== 'SELECT')
                return;

            toggleBlock(this);
            $(this).change(function () {
                toggleBlock(this);
            }); // --change
        }); // --each
    };  // --plugin

})(jQuery);