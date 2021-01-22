const MyMultipleSelect = props => {
  console.log(props);

  function getStyles(name, personName, theme) {
    return {
      fontWeight: personName.indexOf(name) === -1 ? theme.typography.fontWeightRegular : theme.typography.fontWeightMedium
    };
  }

  const useStyles = makeStyles(theme => ({
    formControl: {
      margin: theme.spacing(0) // minWidth: 120,
      // maxWidth: 300,

    },
    chips: {
      display: 'flex',
      flexWrap: 'wrap'
    },
    chip: {
      margin: 2
    },
    noLabel: {
      marginTop: theme.spacing(3)
    }
  }));
  const ITEM_HEIGHT = 48;
  const ITEM_PADDING_TOP = 8;
  const MenuProps = {
    PaperProps: {
      style: {
        maxHeight: 700 //ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        // width: 250,

      }
    }
  };
  const classes = useStyles();

  const handleChange = event => {
    props.setSelectedValue(event.target.value);
  };

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(FormControl, {
    required: true,
    className: `${classes.formControl} col-md-3`
  }, /*#__PURE__*/React.createElement(InputLabel, {
    id: "demo-mutiple-chip-label"
  }, props.selectName), /*#__PURE__*/React.createElement(Select, {
    labelId: "demo-mutiple-chip-label",
    id: "demo-mutiple-chip",
    multiple: true,
    value: props.selectedValue,
    onChange: handleChange,
    input: /*#__PURE__*/React.createElement(Input, {
      id: "select-multiple-chip"
    }),
    renderValue: selected => /*#__PURE__*/React.createElement("div", {
      className: classes.chips
    }, props.selectOptions.flatMap(option => option.questionaryObjects).filter(option => selected.indexOf(option.id) > -1).map(option => /*#__PURE__*/React.createElement(Chip, {
      key: option.id,
      label: option.name,
      className: classes.chip
    }))),
    MenuProps: MenuProps
  }, props.selectOptions?.map(item => [/*#__PURE__*/React.createElement(ListSubheader, null, item.name), item.questionaryObjects.map(object => /*#__PURE__*/React.createElement(MenuItem, {
    key: object.id,
    value: object.id
  }, /*#__PURE__*/React.createElement(Checkbox, {
    checked: props.selectedValue.indexOf(object.id) > -1
  }), /*#__PURE__*/React.createElement(ListItemText, {
    primary: object.name
  })))]))));
};
//# sourceMappingURL=MyMultipleSelect.js.map