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
  const firstStepForm = useForm();
  const selectedCompany = firstStepForm.watch('company');
  const availableObjectTypes = props.selectOptionsTypes.filter(type => type.companyId == selectedCompany);

  const handleClose = () => {
    setOpenAlertGreen(false);
    setOpenAlertRed(false);
  };

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("form", {
    className: classes.root,
    autoComplete: "off"
  }, /*#__PURE__*/React.createElement(MySelect, {
    required: true,
    selectOptions: props.selectOptionsСompanies,
    selectedValue: props.selectedValueСompanies,
    form: firstStepForm,
    name: "company",
    setSelectedValue: selectedValue => {
      props.setSelectedValueTypes(null);
      props.setSelectedValueСompanies(selectedValue);
    },
    nameSwlect: "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u043A\u043E\u043C\u043F\u0430\u043D\u0438\u044E"
  }), /*#__PURE__*/React.createElement(MySelect, {
    required: true,
    name: "objectType",
    selectOptions: availableObjectTypes,
    form: firstStepForm,
    selectedValue: props.selectedValueTypes,
    setSelectedValue: props.setSelectedValueTypes,
    nameSwlect: "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u0442\u0438\u043F \u043E\u0431\u044A\u0435\u043A\u0442\u0430"
  })), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(FormDialogObjectType, {
    form: firstStepForm,
    setOpenAlertRed: setOpenAlertRed,
    setOpenAlertGreen: setOpenAlertGreen,
    setObjectTypes: props.setObjectTypes,
    selectOptionsTypes: props.selectOptionsTypes,
    selectOptions: props.selectOptionsСompanies,
    selectedValue: props.selectedValueСompanies,
    setSelectedValue: props.setSelectedValueСompanies
  })), /*#__PURE__*/React.createElement(Snackbar, {
    open: openAlertGreen,
    autoHideDuration: 6000,
    onClose: handleClose
  }, /*#__PURE__*/React.createElement("div", {
    className: "alert alert-success",
    role: "alert"
  }, "This is a success alert with", /*#__PURE__*/React.createElement("button", {
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
  }, "This is a danger alert with", /*#__PURE__*/React.createElement("button", {
    type: "button",
    className: "close",
    "data-dismiss": "alert",
    "aria-label": "Close"
  }, /*#__PURE__*/React.createElement("span", {
    "aria-hidden": "true"
  }, "\xD7")))));
};
//# sourceMappingURL=FirstStep.js.map