function _extends() { _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; }; return _extends.apply(this, arguments); }

const Transition = React.forwardRef(function Transition(props, ref) {
  return /*#__PURE__*/React.createElement(Slide, _extends({
    direction: "up",
    ref: ref
  }, props));
});

function CloseAlertDialog() {
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return /*#__PURE__*/React.createElement("div", {
    className: "mt-3"
  }, /*#__PURE__*/React.createElement(Button, {
    variant: "outlined",
    color: "secondary",
    onClick: handleClickOpen
  }, "\u041E\u0442\u043C\u0435\u043D\u0430"), /*#__PURE__*/React.createElement(Dialog, {
    open: open,
    TransitionComponent: Transition,
    keepMounted: true,
    onClose: handleClose,
    "aria-labelledby": "alert-dialog-slide-title",
    "aria-describedby": "alert-dialog-slide-description"
  }, /*#__PURE__*/React.createElement(DialogTitle, {
    id: "alert-dialog-slide-title"
  }, "Анкета не будет сохранена, выйти?"), /*#__PURE__*/React.createElement(DialogActions, null, /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u041D\u0435\u0442"), /*#__PURE__*/React.createElement(Button, {
    onClick: handleClose,
    color: "primary"
  }, "\u0414\u0430"))));
}
//# sourceMappingURL=CloseAlertDialog.js.map