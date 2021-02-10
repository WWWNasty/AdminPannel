function _extends() { _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; }; return _extends.apply(this, arguments); }

const FormDialogObject = props => {
  const dialogForm = useForm();
  const form = useFormContext();
  const selectedObjectTypeId = form.watch('objectTypeId');
  const {
    register,
    handleSubmit,
    control,
    reset,
    errors,
    setError
  } = dialogForm;

  const onSubmit = async data => {
    data.objectTypeId = selectedObjectTypeId;
    data.selectedObjectPropertyValues?.forEach(prop => prop.objectPropertyId = Number(prop.objectPropertyId));
    const response = await fetch("/api/QuestionaryObjectApi", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      credentials: "include",
      body: JSON.stringify(data)
    });

    if (response.ok) {
      const result = await response.json();
      props.setOpenAlertGreen(true);
      debugger;
      props.selectedObjectype.questionaryObjects.push(result);
      setOpen(false);
    } else if (response.status == 400) {
      const type = 'oneOrMoreRequired';
      setError('code', {
        type,
        message: 'Введите уникальный код!'
      });
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

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(Button, {
    variant: "outlined",
    color: "primary",
    onClick: handleClickOpen,
    className: "mt-3 mb-2"
  }, "\u0421\u043E\u0437\u0434\u0430\u0442\u044C \u043D\u043E\u0432\u044B\u0439 \u043E\u0431\u044A\u0435\u043A\u0442"), /*#__PURE__*/React.createElement(Dialog, {
    fullWidth: true,
    maxWidth: 'lg',
    open: open,
    onClose: handleClose,
    "aria-labelledby": "form-dialog-title"
  }, /*#__PURE__*/React.createElement(DialogTitle, {
    id: "form-dialog-title"
  }, "\u041D\u043E\u0432\u044B\u0439 \u043E\u0431\u044A\u0435\u043A\u0442"), /*#__PURE__*/React.createElement("form", {
    autoComplete: "off"
  }, /*#__PURE__*/React.createElement(DialogContent, null, /*#__PURE__*/React.createElement(Controller, {
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
  }), /*#__PURE__*/React.createElement(FormControl, _extends({}, props, {
    error: errors?.code?.type,
    fullWidth: true,
    rules: {
      maxLength: {
        message: 'Максимально символов: 20',
        value: 20
      },
      validate: true
    }
  }), /*#__PURE__*/React.createElement(Controller, {
    as: TextField,
    name: "code",
    control: control,
    defaultValue: "",
    required: true,
    margin: "dense",
    id: "standard-required",
    label: "\u041A\u043E\u0434"
  }), /*#__PURE__*/React.createElement(FormHelperText, null, errors.code?.message)), /*#__PURE__*/React.createElement(Controller, {
    as: TextField,
    name: "description",
    control: control,
    defaultValue: "",
    required: true,
    margin: "dense",
    id: "standard-multiline-static",
    label: "\u041E\u043F\u0438\u0441\u0430\u043D\u0438\u0435",
    fullWidth: true,
    multiline: true,
    rows: 4
  }), props.selectedObjectype.objectProperties?.map((item, index) => /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(Controller, {
    as: TextField,
    name: `selectedObjectPropertyValues[${index}].value`,
    control: control,
    defaultValue: "",
    required: true,
    margin: "dense",
    id: "standard-required",
    label: item.name,
    fullWidth: true
  }), /*#__PURE__*/React.createElement("input", {
    type: "hidden",
    ref: register,
    name: `selectedObjectPropertyValues[${index}].objectPropertyId`,
    value: item.id
  })))), /*#__PURE__*/React.createElement(DialogActions, null, /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u041E\u0442\u043C\u0435\u043D\u0430"), /*#__PURE__*/React.createElement(Button, {
    onClick: handleSubmit(onSubmit),
    color: "primary"
  }, "\u0414\u043E\u0431\u0430\u0432\u0438\u0442\u044C")))));
};
//# sourceMappingURL=FormDialogObject.js.map