/// <reference path="jquery-1.7.2-vsdoc.js" />

(function ($) {

    $.fn.toggleBlocks = function () {
        return $(this).each(function () {
            console.log(this.tagName);
            console.log(this);
            if (this.tagName !== 'SELECT')
                return;

            $(this).change(function() {
                $(this).find('option').each(function() {
                    var $this = $(this);
                    var value = $this.val();
                    console.log(value);

                    if ($this.is(':selected')) {
                        $('div[data-depend-value=' + value + ']').show();
                    }
                    else {
                        $('div[data-depend-value=' + value + ']').hide();
                    }
                });
            }); // --change
        }); // --each
    };  // --plugin

})(jQuery);