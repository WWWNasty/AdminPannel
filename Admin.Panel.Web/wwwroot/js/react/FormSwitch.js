function _extends() { _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; }; return _extends.apply(this, arguments); }

const FormSwitch = props => {
  return /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Controller, {
      name: props.name,
      control: props.control,
      defaultValue: props.defaultValue,
      render: ({
        onChange,
        value,
        ...props
      }) => /*#__PURE__*/React.createElement(Switch, _extends({}, props, {
        className: "ml-5",
        checked: value,
        color: "primary",
        onChange: e => onChange(e.target.checked)
      }))
    }),
    label: props.label
  });
};
//# sourceMappingURL=FormSwitch.js.map