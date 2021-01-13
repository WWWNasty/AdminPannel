function FormDialog() {
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
    className: "mt-3"
  }, "\u0421\u043E\u0437\u0434\u0430\u0442\u044C \u043D\u043E\u0432\u044B\u0439"), /*#__PURE__*/React.createElement(Dialog, {
    open: open,
    onClose: handleClose,
    "aria-labelledby": "form-dialog-title"
  }, /*#__PURE__*/React.createElement(DialogTitle, {
    id: "form-dialog-title"
  }, "\u041D\u043E\u0432\u044B\u0439 \u0442\u0438\u043F \u043E\u0431\u044A\u0435\u043A\u0442\u0430"), /*#__PURE__*/React.createElement(DialogContent, null, /*#__PURE__*/React.createElement(DialogContentText, null, "To subscribe to this website, please enter your email address here. We will send updates occasionally."), /*#__PURE__*/React.createElement(TextField, {
    autoFocus: true,
    margin: "dense",
    id: "name",
    label: "Email Address",
    type: "email",
    fullWidth: true
  })), /*#__PURE__*/React.createElement(DialogActions, null, /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u041E\u0442\u043C\u0435\u043D\u0430"), /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u0414\u043E\u0431\u0430\u0432\u0438\u0442\u044C"))));
}
//# sourceMappingURL=DialogForm.js.map