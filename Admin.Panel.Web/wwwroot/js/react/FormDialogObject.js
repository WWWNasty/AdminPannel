const FormDialogObject = props => {
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(Button, {
    variant: "outlined",
    color: "primary",
    onClick: handleClickOpen,
    className: "mt-3 mb-2"
  }, "\u0421\u043E\u0437\u0434\u0430\u0442\u044C \u043D\u043E\u0432\u044B\u0439"), /*#__PURE__*/React.createElement(Dialog, {
    fullWidth: true,
    maxWidth: 'lg',
    open: open,
    onClose: handleClose,
    "aria-labelledby": "form-dialog-title"
  }, /*#__PURE__*/React.createElement(DialogTitle, {
    id: "form-dialog-title"
  }, "\u041D\u043E\u0432\u044B\u0439 \u043E\u0431\u044A\u0435\u043A\u0442"), /*#__PURE__*/React.createElement(DialogContent, null, /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(MySelect, {
    selectOptions: props.selectOptions,
    selectedValue: props.selectedValue,
    setSelectedValue: props.setSelectedValue,
    nameSwlect: "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u043A\u043E\u043C\u043F\u0430\u043D\u0438\u044E"
  })), /*#__PURE__*/React.createElement(TextField, {
    autoFocus: true,
    margin: "dense",
    id: "name",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435",
    fullWidth: true
  }), /*#__PURE__*/React.createElement(TextField, {
    margin: "dense",
    id: "name",
    label: "\u041A\u043E\u0434",
    fullWidth: true
  }), /*#__PURE__*/React.createElement(TextField, {
    id: "standard-multiline-static",
    label: "\u041E\u043F\u0438\u0441\u0430\u043D\u0438\u0435",
    multiline: true,
    rows: 4,
    fullWidth: true
  })), /*#__PURE__*/React.createElement(DialogActions, null, /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u041E\u0442\u043C\u0435\u043D\u0430"), /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u0414\u043E\u0431\u0430\u0432\u0438\u0442\u044C"))));
};
//# sourceMappingURL=FormDialogObject.js.map