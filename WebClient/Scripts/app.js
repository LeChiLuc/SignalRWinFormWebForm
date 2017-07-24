var g_Hub = $.connection.myHub;

g_Hub.client.pushMessage = function (strMessage, name, price) {
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
    console.info('connected,transport=' + g_Hub.connection.transport.name);
})