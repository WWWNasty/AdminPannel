const ThirdStep = props => {
  const form = useFormContext();
  const {
    register,
    handleSubmit,
    control,
    errors
  } = form;
  const questionaryId = form.watch('id');
  const ifQuestionaryCurrentInCompany = form.watch('ifQuestionaryCurrentInCompany');
  const objectTypeId = form.watch('objectTypeId');
  console.log("originObjectTypeId", props.originObjectTypeId, "objectTypeId", objectTypeId);
  return /*#__PURE__*/React.createElement("div", null, questionaryId != null && /*#__PURE__*/React.createElement("div", null, ifQuestionaryCurrentInCompany && props.originObjectTypeId == objectTypeId ? /*#__PURE__*/React.createElement(FormControlLabel, {
    disabled: true,
    control: /*#__PURE__*/React.createElement(Switch, null),
    label: "\u0410\u043D\u043A\u0435\u0442\u0430 \u043D\u0435 \u0430\u043A\u0442\u0438\u0432\u043D\u0430"
  }) : /*#__PURE__*/React.createElement(FormSwitch, {
    name: `isUsed`,
    control: control,
    label: "Анкета активна"
  })), /*#__PURE__*/React.createElement(Controller, {
    error: errors?.name?.type,
    as: TextField,
    autoFocus: true,
    name: "name",
    control: control,
    defaultValue: "",
    required: true,
    margin: "dense",
    id: "standard-required",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435 \u0430\u043D\u043A\u0435\u0442\u044B",
    fullWidth: true,
    rules: {
      required: true,
      maxLength: {
        message: 'Максимально символов: 250',
        value: 250
      },
      validate: true
    },
    helperText: errors?.name?.message
  }), /*#__PURE__*/React.createElement(DraggableComponent, {
    form: form,
    selectableAnswersLists: props.selectableAnswersLists,
    questionaryInputFieldTypes: props.questionaryInputFieldTypes,
    selectableAnswers: props.selectableAnswers
  }));
};
//# sourceMappingURL=ThirdStep.js.map