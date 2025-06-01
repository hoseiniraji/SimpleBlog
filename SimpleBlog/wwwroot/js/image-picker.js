function previewUpload(input, imgTargetSelector) {
    return new Promise((resolve, reject) => {
        var imgSrc = undefined;
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                imgSrc = e.target.result;
                if (!imgTargetSelector) {
                    imgTargetSelector = 'label[for="' + input.id + '"] img';
                }

                $(imgTargetSelector).attr('src', imgSrc);
                resolve(imgSrc);

            }

            reader.readAsDataURL(input.files[0]);
        }
    });

}