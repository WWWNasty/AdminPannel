let count = 1;
$('.addPick').click(function () {

    const template = document.getElementById('voting-option-template');
    const newNode = template.cloneNode(true);

    newNode.removeAttribute('style');
    newNode.removeAttribute('id');
    newNode.dataset.id = count.toString();

    const elements = $(newNode).find('input, select');
    let i1 = $(newNode).find('select.input-answers option');
    $.each(i1, function (index , item){
        item.removeAttribute('selected');
        if(index===0){
            item.setAttribute('selected', 'selected');
        }
    });
    
    console.log(i1);
    count = parseInt($('#voting-options-count').text());
    elements.each((_, input) => {
        let index = count;
        input.setAttribute('id', input.id.replace('0', index.toString()));
        input.setAttribute('itemIndex', index.toString());
        input.setAttribute('name', input.name.replace('0', index.toString()));
    })
    
    newNode.classList.add ('voting-option');
    
    //откуда берем data-id
    let votingOptionSets = $('#simpleList').find(".voting-option-set").toArray();
    //куда записываем индекс
    let inputs = $('#simpleList').find(".sequence-order").toArray();
    let index = 0;
    let i = 0;
    for(index; index < votingOptionSets.length; index++){
        inputs[index].value = index;
        console.log(index, "сортировка записана в модель");
        i = index+1;
    }
    let node = $('#simpleList').append(newNode);
    let seq = $(node).find(".sequence-order").last();
    seq.val(i.toString());
    let z = seq.value;
    getInputFieldTypes();
    count++;
    $('#voting-options-count').text(count);
    console.log(count);
    $(newNode).find('.new-selectpicker').selectpicker();
})

function addDeleteButtonHandler() {
    $('.deletePick').click(deleteVotingOption);
}

function deleteVotingOption() {
    if (count > 1) {
        $('.voting-option').last().remove();
        count--;
    }
}

$(document).ready(async () => {
    count = parseInt($('#voting-options-count').text());
   
    firstSequenceOrder();
    addDeleteButtonHandler();
})

function firstSequenceOrder(){
    const container = $('#simpleList');
    let inputContainer = $(container).find(".aa-container");
    let input = $(inputContainer).find("#init-sequence-order")
    input.val("0");
}

function getInputFieldTypes() {
    let input = $('.input-answers');
    input.change(loadAnswers);
}
