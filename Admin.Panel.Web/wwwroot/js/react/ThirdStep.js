const ThirdStep = props => {
  const form = useFormContext();
  console.log(form);
  const {
    register,
    handleSubmit,
    control,
    errors
  } = form;
  const questionaryId = form.watch('id');
  console.log(errors);
  return /*#__PURE__*/React.createElement("div", null, questionaryId != null && /*#__PURE__*/React.createElement(FormSwitch, {
    name: `isUsed`,
    control: control,
    label: "Анкета активна"
  }), /*#__PURE__*/React.createElement(Controller, {
    error: errors.name?.type,
    as: TextField,
    autoFocus: true,
    name: "name",
    control: control,
    defaultValue: "",
    required: true,
    margin: "dense",
    id: "standard-required",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435",
    fullWidth: true //required={ {message: '', value: true} }
    ,
    rules: {
      required: true,
      maxLength: 250,
      validate: true
    }
  }), /*#__PURE__*/React.createElement(DraggableComponent, {
    form: form,
    selectableAnswersLists: props.selectableAnswersLists,
    questionaryInputFieldTypes: props.questionaryInputFieldTypes,
    selectableAnswers: props.selectableAnswers
  }));
};
//# sourceMappingURL=ThirdStep.js.map