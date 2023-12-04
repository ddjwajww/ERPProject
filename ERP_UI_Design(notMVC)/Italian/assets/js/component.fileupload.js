(function ($) {
    "use strict";

    function FileUpload() {
        this.$body = $("body");
    }

    FileUpload.prototype.init = function () {
        Dropzone.autoDiscover = false;

        $('[data-plugin="dropzone"]').each(function () {
            var $this = $(this);
            var url = $this.attr("action");
            var previewsContainer = $this.data("previewsContainer");
            var uploadPreviewTemplate = $this.data("uploadPreviewTemplate");
            var options = { url: url };

            if (previewsContainer) {
                options.previewsContainer = previewsContainer;
            }

            if (uploadPreviewTemplate) {
                options.previewTemplate = $(uploadPreviewTemplate).html();
            }

            $this.dropzone(options);
        });
    };

    window.jQuery.FileUpload = new FileUpload();
    window.jQuery.FileUpload.Constructor = FileUpload;
})();

(function () {
    "use strict";
    window.jQuery.FileUpload.init();
})();
