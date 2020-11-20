let countAnsw = 1;
$('.add').click(function () {

    const template = document.getElementById('option-template');
    const newNode = template.cloneNode(true);

    newNode.removeAttribute('style');
    newNode.removeAttribute('id');
    newNode.dataset.id = countAnsw.toString();


    const elements = $(newNode).find('input');
    countAnsw = parseInt($('#answer-options-count').text());
    elements.each((_, input) => {
        let index = countAnsw;
        input.setAttribute('id', input.id.replace('0', index.toString()));
        input.setAttribute('name', input.name.replace('0', index.toString()));
    })


    //const spans = $(newNode).find('span');

    //spans.each((_, span) => span.setAttribute('data-valmsg-for', span.data_valmsg_for.replace('0', countAnsw.toString())));

    newNode.classList.add('answer-option');
debugger;
    //откуда берем data-id
    let votingOptionSets = $('#simpleList').find(".voting-option-set").toArray();
    //куда записываем индекс
    let inputs = $('#simpleList').find(".sequence-order").toArray();
    let index = 0;
    let i = 0;
    for(index; index < inputs.length; index++){
        inputs[index].value = index;
        console.log(index, "сортировка записана в модель");
        i = index+1;
    }
    
    let node = $('#simpleList').append(newNode);

    let seq = $(node).find(".sequence-order").last();
    seq.val(i.toString());
    let z = seq.value;
    countAnsw++;
    let c = $('#answer-options-count');
        c.text(countAnsw);
    console.log(countAnsw);
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
    firstSequenceOrder();
    addDeleteButtonHandler();
})
function firstSequenceOrder(){
    debugger;
    const container = $('#simpleList');
    let inputContainer = $(container).find(".aa-container");
    let input = $(inputContainer).find("#init-sequence-order")
    input.val("0");
}