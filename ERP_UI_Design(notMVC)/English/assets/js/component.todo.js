(function ($) {
    "use strict";
  
    function TodoApp() {
      this.$body = $("body");
      this.$todoContainer = $("#todo-container");
      this.$todoMessage = $("#todo-message");
      this.$todoRemaining = $("#todo-remaining");
      this.$todoTotal = $("#todo-total");
      this.$archiveBtn = $("#btn-archive");
      this.$todoList = $("#todo-list");
      this.$todoDonechk = ".todo-done";
      this.$todoForm = $("#todo-form");
      this.$todoInput = $("#todo-input-text");
      this.$todoBtn = $("#todo-btn-submit");
      this.$todoData = [
        { id: "1", text: "Design One page theme", done: false },
        { id: "2", text: "Build a js based app", done: true },
        { id: "3", text: "Creating component page", done: true },
        { id: "4", text: "Testing??", done: true },
        { id: "5", text: "Hehe!! This looks cool!", done: false },
        { id: "6", text: "Create new version 3.0", done: false },
        { id: "7", text: "Build an angular app", done: true },
      ];
      this.$todoCompletedData = [];
      this.$todoUnCompletedData = [];
    }
  
    TodoApp.prototype.markTodo = function (id, done) {
      for (var i = 0; i < this.$todoData.length; i++) {
        if (this.$todoData[i].id == id) {
          this.$todoData[i].done = done;
        }
      }
    };
  
    TodoApp.prototype.addTodo = function (text) {
      this.$todoData.push({ id: this.$todoData.length, text: text, done: false });
      this.generate();
    };
  
    TodoApp.prototype.archives = function () {
      this.$todoUnCompletedData = [];
      for (var i = 0; i < this.$todoData.length; i++) {
        var todo = this.$todoData[i];
        if (todo.done == 1) {
          this.$todoCompletedData.push(todo);
        } else {
          this.$todoUnCompletedData.push(todo);
        }
      }
      this.$todoData = [];
      this.$todoData = [].concat(this.$todoUnCompletedData);
      this.generate();
    };
  
    TodoApp.prototype.generate = function () {
      this.$todoList.html("");
      var remainingCount = 0;
  
      for (var i = 0; i < this.$todoData.length; i++) {
        var todo = this.$todoData[i];
        if (todo.done == 1) {
          this.$todoList.prepend('<li class="list-group-item border-0 ps-0"><div class="form-check mb-0"><input type="checkbox" class="form-check-input todo-done" id="' + todo.id + '" checked><label class="form-check-label" for="' + todo.id + '"><s>' + todo.text + "</s></label></div></li>");
        } else {
          remainingCount += 1;
          this.$todoList.prepend('<li class="list-group-item border-0 ps-0"><div class="form-check mb-0"><input type="checkbox" class="form-check-input todo-done" id="' + todo.id + '"><label class="form-check-label" for="' + todo.id + '">' + todo.text + "</label></div></li>");
        }
      }
  
      this.$todoTotal.text(this.$todoData.length);
      this.$todoRemaining.text(remainingCount);
    };
  
    TodoApp.prototype.init = function () {
      var self = this;
      this.generate();
  
      this.$archiveBtn.on("click", function (event) {
        event.preventDefault();
        self.archives();
        return false;
      });
  
      $(document).on("change", this.$todoDonechk, function () {
        if (this.checked) {
          self.markTodo($(this).attr("id"), true);
        } else {
          self.markTodo($(this).attr("id"), false);
        }
        self.generate();
      });
  
      this.$todoForm.on("submit", function (event) {
        event.preventDefault();
        if (self.$todoInput.val() == "" || typeof self.$todoInput.val() === "undefined" || self.$todoInput.val() == null) {
          self.$todoInput.focus();
          return false;
        } else {
          self.addTodo(self.$todoInput.val());
          self.$todoForm.removeClass("was-validated");
          self.$todoInput.val("");
          return true;
        }
      });
    };
  
    window.jQuery.TodoApp = new TodoApp();
    window.jQuery.TodoApp.Constructor = TodoApp;
  })();
  
  (function () {
    "use strict";
    window.jQuery.TodoApp.init();
  })();
  