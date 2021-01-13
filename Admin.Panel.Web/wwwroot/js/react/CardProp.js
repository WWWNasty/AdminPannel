function CardProp() {
  const classes = useStyles();
  const [state, setState] = React.useState({
    checked: true
  });

  const handleChange = event => {
    setState({ ...state,
      [event.target.name]: event.target.checked
    });
  };

  return /*#__PURE__*/React.createElement(Card, {
    className: `${classes.root} mt-3 mb-3 bg-light`
  }, /*#__PURE__*/React.createElement(CardContent, null, /*#__PURE__*/React.createElement(Typography, {
    className: classes.pos,
    color: "textSecondary"
  }, /*#__PURE__*/React.createElement("div", {
    className: 'd-flex'
  }, "\u0441\u0432\u043E\u0439\u0441\u0442\u0432\u043E", /*#__PURE__*/React.createElement(IconButton, {
    "aria-label": "delete",
    className: `${classes.margin} ml-auto`
  }, /*#__PURE__*/React.createElement(Icon, null, "delete")))), /*#__PURE__*/React.createElement(TextField, {
    id: "standard-basic",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435"
  }), /*#__PURE__*/React.createElement(TextField, {
    id: "standard-basic",
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435 \u0432 \u043E\u0442\u0447\u0435\u0442\u0435"
  }), /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Checkbox, {
      checked: state.checked,
      onChange: handleChange,
      name: "checkedB",
      color: "primary"
    }),
    label: "\u042D\u0442\u043E \u043F\u043E\u043B\u0435 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u0443\u0435\u0442\u0441\u044F \u0432 \u043E\u0442\u0447\u0435\u0442\u0435"
  })));
}
//# sourceMappingURL=CardProp.js.map