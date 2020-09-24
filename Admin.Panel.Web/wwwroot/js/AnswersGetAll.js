$('#QuestionaryQuestions[0].SelectableAnswersListId').change(async event => {
    const id = event.target.value;

    const partial = await $.get(`https://localhost:5001/Questionary/_SelectableAnswers/${id}`);

    $("#option-container").html(partial);
})