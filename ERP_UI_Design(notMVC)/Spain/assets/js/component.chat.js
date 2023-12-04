(function ($) {
    "use strict";

    function ChatApp() {
        this.$body = $("body");
        this.$chatInput = $(".chat-input");
        this.$chatList = $(".conversation-list");
        this.$chatSendBtn = $(".chat-send");
        this.$chatForm = $("#chat-form");
    }

    ChatApp.prototype.save = function () {
        var message = this.$chatInput.val();
        var time = moment().format("h:mm");

        if (message === "") {
            this.$chatInput.focus();
            return false;
        }

        $('<li class="clearfix odd"><div class="chat-avatar"><img src="assets/images/users/avatar-1.jpg" alt="male"><i>' + time + '</i></div><div class="conversation-text"><div class="ctext-wrap"><i>Dominic</i><p>' + message + '</p></div></div></li>').appendTo(".conversation-list");

        this.$chatInput.focus();
        this.$chatList.animate({ scrollTop: this.$chatList.prop("scrollHeight") }, 1000);

        return true;
    };

    ChatApp.prototype.init = function () {
        var self = this;

        self.$chatInput.keypress(function (event) {
            if (event.which === 13) {
                self.save();
                return false;
            }
        });

        self.$chatForm.on("submit", function (event) {
            event.preventDefault();
            self.save();
            self.$chatForm.removeClass("was-validated");
            self.$chatInput.val("");
            return false;
        });
    };

    window.jQuery.ChatApp = new ChatApp();
    window.jQuery.ChatApp.Constructor = ChatApp;
})();

(function () {
    "use strict";
    window.jQuery.ChatApp.init();
})();
