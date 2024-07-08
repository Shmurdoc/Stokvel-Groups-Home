
    const imageInput = document.getElementById('imageInput');
    const previewImage = document.getElementById('previewImage');
    function previewSelectedImage() {
    const file = imageInput.files[0];
    if (file) {
         document.getElementById('imgname').value = null;
     const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function (e) {
        previewImage.src = e.target.result;
                                                        }
                                                    }
                                                }
    imageInput.addEventListener('change', previewSelectedImage);

