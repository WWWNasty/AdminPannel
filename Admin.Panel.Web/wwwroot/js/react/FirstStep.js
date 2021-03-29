const FirstStep = props => {
  const useStyles = makeStyles(theme => createStyles({
    root: {
      '& .MuiTextField-root': {
        margin: theme.spacing(1),
        width: 200
      }
    }
  }));
  const classes = useStyles();
  const [openAlertGreen, setOpenAlertGreen] = React.useState(false);
  const [openAlertRed, setOpenAlertRed] = React.useState(false);
  const form = useFormContext();
  const {
    handleSubmit
  } = form;
  const selectedCompany = form.watch('companyId');
  const availableObjectTypes = props.selectOptionsTypes.filter(type => type.companyId == selectedCompany);

  const onChange = () => {
    props.setObjectTypeId(null);
    form.setValue('objectTypeId', null);
    console.log(form.watch('objectTypeId'), 'obj id');
  };

  const handleClose = () => {
    setOpenAlertGreen(false);
    setOpenAlertRed(false);
  };

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("form", {
    className: classes.root,
    autoComplete: "off"
  }, /*#__PURE__*/React.createElement(MySelect, {
    required: {
      message: '',
      value: true
    },
    selectOptions: props.selectOptionsСompanies,
    form: form,
    onChange: onChange,
    name: "companyId",
    nameSwlect: "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u043A\u043E\u043C\u043F\u0430\u043D\u0438\u044E"
  }), /*#__PURE__*/React.createElement(MySelect, {
    required: true,
    name: "objectTypeId",
    selectOptions: availableObjectTypes,
    form: form,
    nameSwlect: "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u0442\u0438\u043F \u043E\u0431\u044A\u0435\u043A\u0442\u0430"
  })), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(FormDialogObjectType, {
    form: form,
    setOpenAlertRed: setOpenAlertRed,
    setOpenAlertGreen: setOpenAlertGreen,
    setObjectTypes: props.setObjectTypes,
    selectOptionsTypes: props.selectOptionsTypes,
    selectOptions: props.selectOptionsСompanies
  })), /*#__PURE__*/React.createElement(Snackbar, {
    open: openAlertGreen,
    autoHideDuration: 6000,
    onClose: handleClose
  }, /*#__PURE__*/React.createElement("div", {
    className: "alert alert-success",
    role: "alert"
  }, "\u0422\u0438\u043F \u043E\u0431\u044A\u0435\u043A\u0442\u043E\u0432 \u0443\u0441\u043F\u0435\u0448\u043D\u043E \u0441\u043E\u0437\u0434\u0430\u043D!", /*#__PURE__*/React.createElement("button", {
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
  }, "\u041F\u0440\u043E\u0438\u0437\u043E\u0448\u043B\u0430 \u043E\u0448\u0438\u0431\u043A\u0430, \u0442\u0438\u043F \u043E\u0431\u044A\u0435\u043A\u0442\u0430 \u043D\u0435 \u0441\u043E\u0437\u0434\u0430\u043D!", /*#__PURE__*/React.createElement("button", {
    type: "button",
    className: "close",
    "data-dismiss": "alert",
    "aria-label": "Close"
  }, /*#__PURE__*/React.createElement("span", {
    "aria-hidden": "true"
  }, "\xD7")))));
};
//# sourceMappingURL=FirstStep.js.map