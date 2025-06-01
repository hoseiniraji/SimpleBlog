function showUniqueNameHint(uniqueName) {
    if (uniqueName) {
        uniqueName = uniqueName.trim().replace(/[\s\/]/g, '-');
        $('#UniqueNameHint').text(`${window.location.origin}/${uniqueName}`);
    }

}

$(() => {

    $('#Token').keyup(e => {
        var uniqueName = $(e.target).val() || '';
        showUniqueNameHint(uniqueName);
        $('#auto-update-token').val('0')
    })

    $('#Title').keyup(e => {
        var uniqueName = $(e.target).val() || '';
        uniqueName = uniqueName.replace(/\s+/g, '-');
        var autoUpdate = $('#auto-update-token').val() == "1";
        if (autoUpdate) {
            $('#Token').attr('placeholder', uniqueName).val(uniqueName);
            showUniqueNameHint(uniqueName);
        }
    })


    showUniqueNameHint($('#Token').val());


})

