function _extends() { _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; }; return _extends.apply(this, arguments); }

const ReactHookFormSelect = ({
  name,
  label,
  control,
  defaultValue,
  children,
  ...props
}) => {
  const {
    errors
  } = useFormContext();
  const labelId = `${name}-label`;
  return /*#__PURE__*/React.createElement(FormControl, _extends({}, props, {
    error: errors[name]?.type
  }), /*#__PURE__*/React.createElement(InputLabel, {
    id: labelId
  }, label), /*#__PURE__*/React.createElement(Controller, {
    as: /*#__PURE__*/React.createElement(Select, {
      renderValue: props.renderValue,
      multiple: props.multiple,
      labelId: labelId,
      label: label
    }, children),
    rules: {
      required: props.required,
      minLength: props.minLength,
      validate: props.validate
    },
    name: name,
    control: control,
    defaultValue: defaultValue
  }), /*#__PURE__*/React.createElement(FormHelperText, null, errors[name]?.message));
};

const MySelect = props => {
  const classes = useStyles();
  const {
    control
  } = props.form ?? useFormContext();
  return /*#__PURE__*/React.createElement(FormControl, {
    className: `${classes.formControl} col-md-3 mr-3`
  }, /*#__PURE__*/React.createElement(ReactHookFormSelect, {
    required: props.required,
    name: props.name,
    label: props.nameSwlect,
    defaultValue: props.selectedValue,
    className: classes.selectEmpty,
    control: control
  }, props.selectOptions?.map(item => /*#__PURE__*/React.createElement(MenuItem, {
    value: item.id
  }, item.name)) ?? []));
};
//# sourceMappingURL=MySelect.js.map