const DraggableComponent = props => {
  const form = useFormContext();
  const questionsFieldName = `questionaryQuestions`;
  const {
    remove,
    append,
    move,
    fields
  } = useFieldArray({
    control: form.control,
    name: questionsFieldName,
    keyName: 'key'
  });

  const onDragEnd = result => {
    // dropped outside the list
    if (!result.destination) {
      return;
    }

    move(result.source.index, result.destination.index); //form.setValue(`questionaryQuestions[${props.index}].sequenceOrder`, result.destination.index)
  };

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(DragDropContext, {
    onDragEnd: onDragEnd
  }, /*#__PURE__*/React.createElement(Droppable, {
    droppableId: "droppable"
  }, (provided, snapshot) => /*#__PURE__*/React.createElement(RootRef, {
    rootRef: provided.innerRef
  }, /*#__PURE__*/React.createElement(List, {
    style: getListStyle(snapshot.isDraggingOver)
  }, fields.map((question, index) => /*#__PURE__*/React.createElement(Draggable, {
    key: question.key,
    index: index,
    draggableId: question.key?.toString()
  }, (provided, snapshot) => /*#__PURE__*/React.createElement(DraggableCard, {
    question: question,
    form: form,
    selectableAnswersLists: props.selectableAnswersLists,
    questionaryInputFieldTypes: props.questionaryInputFieldTypes,
    selectableAnswers: props.selectableAnswers,
    provided: provided,
    removeQuestion: () => remove(index),
    snapshot: snapshot,
    index: index
  }))), provided.placeholder)))), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(IconButton, {
    onClick: () => append({
      key: Math.random(),
      questionText: '',
      canSkipQuestion: false,
      selectableAnswersListId: null,
      questionaryInputFieldTypeId: null,
      defaultAnswerId: null,
      questionaryAnswerOptions: []
    }),
    color: "primary",
    "aria-label": "add",
    className: "mt-50 mb-50 ml-50"
  }, /*#__PURE__*/React.createElement(Icon, null, "add"), " ", /*#__PURE__*/React.createElement("h6", {
    className: "mt-2"
  }, "\u0414\u043E\u0431\u0430\u0432\u0438\u0442\u044C \u0432\u043E\u043F\u0440\u043E\u0441"))));
}; // fake data generator


const getItems = count => Array.from({
  length: count
}, (v, k) => k).map(k => ({
  id: `item-${k}`,
  primary: `item ${k}`,
  secondary: k % 2 === 0 ? `Whatever for ${k}` : undefined
})); // a little function to help us with reordering the result


const reorder = (list, startIndex, endIndex) => {
  const result = Array.from(list);
  const [removed] = result.splice(startIndex, 1);
  result.splice(endIndex, 0, removed);
  return result;
};

const getItemStyle = (isDragging, draggableStyle) => ({ // styles we need to apply on draggables
  ...draggableStyle,
  ...(isDragging && {
    background: "rgb(235,235,235)"
  })
});

const getListStyle = isDraggingOver => ({//background: isDraggingOver ? 'lightblue' : 'lightgrey'
});
//# sourceMappingURL=DraggableComponent.js.map