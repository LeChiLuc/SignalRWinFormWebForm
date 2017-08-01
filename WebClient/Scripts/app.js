var hub = $.connection.myHub;

hub.client.pushMessage = function (strMessage, name, price) {
    console.info(strMessage, name, price);

    var html = '';
    var template = $('#data-template').html();
        html += Mustache.render(template, {
            MaCK: strMessage,
            TenCT: name,
            Gia: price
        });
    $('#tblData').html(html);
}

$.connection.hub.start().done(function () {
    console.info('connected,transport=' + hub.connection.transport.name);
})