function FormDialogObjectType(props) {
  const dialogForm = useForm();
  const {
    register,
    handleSubmit,
    control,
    reset,
    errors
  } = dialogForm;

  const onSubmit = async data => {
    const response = await fetch("/api/ObjectTypeApi", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      credentials: "include",
      body: JSON.stringify(data)
    });
    const newObjectType = await response.json();
    props.setObjectTypes([newObjectType, ...props.selectOptionsTypes]);
    reset();
    setOpen(false);

    if (response.ok) {
      props.setOpenAlertGreen(true);
    } else {
      props.setOpenAlertRed(true);
    }
  };

  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    reset();
  };

  const objectProperties = `objectProperties`;
  const {
    remove,
    append,
    fields
  } = useFieldArray({
    control: dialogForm.control,
    name: objectProperties
  });
  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(Button, {
    variant: "outlined",
    color: "primary",
    onClick: handleClickOpen,
    className: "mt-3 mb-2"
  }, "\u0421\u043E\u0437\u0434\u0430\u0442\u044C \u043D\u043E\u0432\u044B\u0439 \u0442\u0438\u043F \u043E\u0431\u044A\u0435\u043A\u0442\u0430"), /*#__PURE__*/React.createElement(Dialog, {
    fullWidth: true,
    maxWidth: 'lg',
    open: open,
    onClose: handleClose,
    "aria-labelledby": "form-dialog-title"
  }, /*#__PURE__*/React.createElement(DialogTitle, {
    id: "form-dialog-title"
  }, "\u041D\u043E\u0432\u044B\u0439 \u0442\u0438\u043F \u043E\u0431\u044A\u0435\u043A\u0442\u0430"), /*#__PURE__*/React.createElement("form", {
    autoComplete: "off"
  }, /*#__PURE__*/React.createElement(DialogContent, null, /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(MySelect, {
    error: errors?.companyId?.type,
    autoFocus: true,
    required: {
      message: '',
      value: true
    },
    name: "companyId",
    selectOptions: props.selectOptions,
    selectedValue: props.selectedValue,
    form: dialogForm,
    setSelectedValue: props.setSelectedValue,
    nameSwlect: "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u043A\u043E\u043C\u043F\u0430\u043D\u0438\u044E"
  })), /*#__PURE__*/React.createElement(Controller, {
    error: errors?.name?.type,
    as: TextField,
    name: "name",
    control: control,
    defaultValue: "",
    required: true,
    margin: "dense",
    id: "standard-required",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435 \u0442\u0438\u043F\u0430 \u043E\u0431\u044A\u0435\u043A\u0442\u0430",
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
  }), /*#__PURE__*/React.createElement("div", null, fields.map((property, index) => {
    return /*#__PURE__*/React.createElement(CardProp, {
      remove: () => remove(index),
      key: property.key,
      index: index,
      form: dialogForm,
      registerForm: register
    });
  })), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(IconButton, {
    color: "primary",
    "aria-label": "add",
    onClick: () => append({
      key: Math.random(),
      name: '',
      isUsedInReport: false,
      nameInReport: ''
    })
  }, /*#__PURE__*/React.createElement(Icon, null, "add"), " ", /*#__PURE__*/React.createElement("h6", {
    className: "mt-2 font-weight-light"
  }, "\u0414\u043E\u0431\u0430\u0432\u0438\u0442\u044C \u0441\u0432\u043E\u0439\u0441\u0442\u0432\u043E")))), /*#__PURE__*/React.createElement(DialogActions, null, /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u041E\u0442\u043C\u0435\u043D\u0430"), /*#__PURE__*/React.createElement(Button, {
    onClick: handleSubmit(onSubmit),
    color: "primary"
  }, "\u0414\u043E\u0431\u0430\u0432\u0438\u0442\u044C")))));
}
//# sourceMappingURL=FormDialogObjectType.js.map