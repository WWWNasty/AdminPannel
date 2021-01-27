const ThirdStep = props => {
  const {
    register,
    handleSubmit,
    control
  } = useFormContext();

  const onSubmit = data => {
    console.log(data);
  };

  const form = useFormContext();
  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("form", {
    onSubmit: handleSubmit(onSubmit)
  }, /*#__PURE__*/React.createElement(Controller, {
    as: TextField,
    autoFocus: true,
    name: "name",
    control: control,
    defaultValue: "",
    required: true,
    margin: "dense",
    id: "standard-required",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435",
    fullWidth: true
  }), /*#__PURE__*/React.createElement(DraggableComponent, {
    form: form,
    selectableAnswersLists: props.selectableAnswersLists,
    questionaryInputFieldTypes: props.questionaryInputFieldTypes,
    selectableAnswers: props.selectableAnswers
  })));
};
//# sourceMappingURL=ThirdStep.js.map