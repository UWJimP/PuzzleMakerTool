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
mergeInto(LibraryManager.library, ImageUploaderPlugin);
