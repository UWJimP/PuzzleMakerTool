var ImageUploaderPlugin = {
  ImageUploaderInit: function() {
    var fileInput = document.createElement('input');
    fileInput.setAttribute('type', 'file');
    fileInput.onclick = function (event) {
      this.value = null;
    };
    fileInput.onchange = function (event) {
      SendMessage('Canvas', 'FileAngularSelect', URL.createObjectURL(event.target.files[0]));
    }
    //document.body.appendChild(fileInput);
  }
};
var MenuSelection = {

  FinishLoading: function() {
	window['menu'].component.finishLoading();
  },
  StartLoading: function() {
	window['menu'].component.startLoading();
  },
  ChangeMenu: function(value) {
	window['menu'].component.changeMenu(value);
  },
  SendAngularError: function(error) {
	window.alert(Pointer_stringify(error));
  },
  SendAngularPuzzleCode: function(code) {
	window['code'].component.receiveCode(Pointer_stringify(code));
  }
}
mergeInto(LibraryManager.library, MenuSelection);
