function _extends() { _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; }; return _extends.apply(this, arguments); }

const SlideTransition = React.forwardRef(function Transition(props, ref) {
  return /*#__PURE__*/React.createElement(Slide, _extends({
    direction: "up",
    ref: ref
  }, props));
});

function CloseAlertDialog(props) {
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = redirectToAll => {
    setOpen(false);

    if (redirectToAll === true) {
      window.location = props.getAllRoute;
    }
  };

  return /*#__PURE__*/React.createElement("div", {
    className: "d-flex justify-content-around"
  }, /*#__PURE__*/React.createElement(Button, {
    className: "d-flex justify-content-end",
    variant: "outlined",
    color: "secondary",
    onClick: handleClickOpen
  }, "\u0412\u044B\u0439\u0442\u0438 \u0431\u0435\u0437 \u0441\u043E\u0445\u0440\u0430\u043D\u0435\u043D\u0438\u044F"), /*#__PURE__*/React.createElement(Dialog, {
    open: open,
    TransitionComponent: SlideTransition,
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
    onClick: () => handleClose(true),
    color: "primary"
  }, "\u0414\u0430"))));
}
//# sourceMappingURL=CloseAlertDialog.js.map