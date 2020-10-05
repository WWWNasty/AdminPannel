let input = $('.input-answers');

input.change(async event => {
    const id = event.target.value;
    
    const index = event.target.attributes.itemIndex.value;
    
    debugger;

    const partial = await $.get(`/Questionary/AnswersGetAll/${id}?index=${index}`);

    const optionContainer = $(event.target.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).find('.option-container');
    
    optionContainer.html(partial);
    $(optionContainer).find('.new-selectpicker').selectpicker();
}) 