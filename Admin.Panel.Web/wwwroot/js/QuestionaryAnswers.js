let countAnsw = 1;
$('.add').click(function () {

    const template = document.getElementById('option-template');
    const newNode = template.cloneNode(true);

    newNode.removeAttribute('style');
    newNode.removeAttribute('id');

    const elements = $(newNode).find('input');

    elements.each((_, input) => {
        input.setAttribute('id', input.id.replace('0', countAnsw.toString()));
        input.setAttribute('name', input.name.replace('0', countAnsw.toString()));
    })


    //const spans = $(newNode).find('span');

    //spans.each((_, span) => span.setAttribute('data-valmsg-for', span.data_valmsg_for.replace('0', countAnsw.toString())));

    newNode.classList.add('answer-option');

    let create = $('.add-container').before(newNode);
    countAnsw++;

})

function addDeleteButtonHandler() {

    $('.delete').click(deleteVotingOption);
}

function deleteVotingOption() {
    debugger;
    if (countAnsw > 1) {
        let del = $('.answer-option').last().remove();
        countAnsw--;
    }
}

$(document).ready(async () => {
    countAnsw = parseInt($('#answer-options-count').text());
    addDeleteButtonHandler();
})