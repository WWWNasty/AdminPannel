const SecondStep = props => {
  const form = useFormContext();
  const [openAlertGreen, setOpenAlertGreen] = React.useState(false);
  const [openAlertRed, setOpenAlertRed] = React.useState(false);

  const handleClose = () => {
    setOpenAlertGreen(false);
    setOpenAlertRed(false);
  };

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(MyMultipleSelect, {
    form: form,
    selectOptions: props.selectOptions,
    selectedValue: props.selectedValue,
    selectName: "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u043E\u0431\u044A\u0435\u043A\u0442\u044B \u0434\u043B\u044F \u0430\u043D\u043A\u0435\u0442\u044B"
  }), /*#__PURE__*/React.createElement(FormDialogObject, {
    objectTypes: props.selectOptions,
    selectedObjectype: props.selectedObjectype,
    setOpenAlertRed: setOpenAlertRed,
    setOpenAlertGreen: setOpenAlertGreen
  }), /*#__PURE__*/React.createElement(Snackbar, {
    open: openAlertGreen,
    autoHideDuration: 6000,
    onClose: handleClose
  }, /*#__PURE__*/React.createElement("div", {
    className: "alert alert-success",
    role: "alert"
  }, "\u041E\u0431\u044A\u0435\u043A\u0442 \u0443\u0441\u043F\u0435\u0448\u043D\u043E \u0441\u043E\u0437\u0434\u0430\u043D!", /*#__PURE__*/React.createElement("button", {
    type: "button",
    className: "close",
    "data-dismiss": "alert",
    "aria-label": "Close"
  }, /*#__PURE__*/React.createElement("span", {
    "aria-hidden": "true"
  }, "\xD7")))), /*#__PURE__*/React.createElement(Snackbar, {
    open: openAlertRed,
    autoHideDuration: 6000,
    onClose: handleClose
  }, /*#__PURE__*/React.createElement("div", {
    className: "alert alert-danger",
    role: "alert"
  }, "\u041F\u0440\u043E\u0438\u0437\u043E\u0448\u043B\u0430 \u043E\u0448\u0438\u0431\u043A\u0430, \u043E\u0431\u044A\u0435\u043A\u0442 \u043D\u0435 \u0441\u043E\u0437\u0434\u0430\u043D!", /*#__PURE__*/React.createElement("button", {
    type: "button",
    className: "close",
    "data-dismiss": "alert",
    "aria-label": "Close"
  }, /*#__PURE__*/React.createElement("span", {
    "aria-hidden": "true"
  }, "\xD7")))));
};
//# sourceMappingURL=SecondStep.js.map