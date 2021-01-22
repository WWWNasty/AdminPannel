const ThirdStep = () => {
  const {
    register,
    handleSubmit
  } = useForm();

  const onSubmit = data => {
    console.log(data);
  };

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("form", {
    onSubmit: handleSubmit(onSubmit)
  }, /*#__PURE__*/React.createElement(TextField, {
    required: true,
    autoFocus: true,
    margin: "dense",
    id: "standard-required",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435",
    fullWidth: true
  }), /*#__PURE__*/React.createElement(DraggableComponent, null)));
};
//# sourceMappingURL=ThirdStep.js.map