function FormDialogObjectType(props) {
  const [indexes, setIndexes] = React.useState([]);
  const [counter, setCounter] = React.useState(0);
  const dialogForm = useForm();
  const {
    register,
    handleSubmit,
    control
  } = dialogForm;

  const onSubmit = data => {
    console.log(data);
    const response = fetch("/api/ObjectTypeApi", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      credentials: "include",
      body: JSON.stringify(data)
    });
    props.setObjectTypes([data, ...props.selectOptionsTypes]);
    setOpen(false);
    setCounter(0);

    if (response) {
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
  };

  const addFriend = () => {
    setIndexes(prevIndexes => [...prevIndexes, counter]);
    setCounter(prevCounter => prevCounter + 1);
  };

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
    onSubmit: handleSubmit(onSubmit),
    autoComplete: "off"
  }, /*#__PURE__*/React.createElement(DialogContent, null, /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(MySelect, {
    autoFocus: true,
    required: true,
    name: "companyId",
    selectOptions: props.selectOptions,
    selectedValue: props.selectedValue,
    form: dialogForm,
    setSelectedValue: props.setSelectedValue,
    nameSwlect: "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u043A\u043E\u043C\u043F\u0430\u043D\u0438\u044E"
  })), /*#__PURE__*/React.createElement(Controller, {
    as: TextField,
    name: "name",
    control: control,
    defaultValue: "",
    required: true,
    margin: "dense",
    id: "standard-required",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435",
    fullWidth: true
  }), /*#__PURE__*/React.createElement("div", null, indexes.map(index => {
    return /*#__PURE__*/React.createElement(CardProp, {
      index: index,
      setIndexes: setIndexes,
      setCounter: setCounter,
      form: dialogForm,
      registerForm: register
    });
  })), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(IconButton, {
    color: "primary",
    "aria-label": "add",
    onClick: addFriend
  }, /*#__PURE__*/React.createElement(Icon, null, "add")))), /*#__PURE__*/React.createElement(DialogActions, null, /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u041E\u0442\u043C\u0435\u043D\u0430"), /*#__PURE__*/React.createElement(Button, {
    type: "submit",
    color: "primary"
  }, "\u0414\u043E\u0431\u0430\u0432\u0438\u0442\u044C")))));
}
//# sourceMappingURL=FormDialogObjectType.js.map