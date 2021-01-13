const FirstStep = (props) => {

    return (
        <div>
            <div><MySelect selectOptions = {props.selectOptionsСompanies} selectedValue = {props.selectedValueСompanies} setSelectedValue = {props.setSelectedValueTypes} nameSwlect = "Выберите компанию" /></div>
            <div><MySelect selectOptions = {props.selectOptionsTypes} selectedValue = {props.selectedValueTypes} setSelectedValue = {props.setSelectedValueTypes} nameSwlect = "Выберите тип объекта" /></div>
            <div><FormDialogObjectType selectOptions = {props.selectOptionsСompanies} selectedValue = {props.selectedValueСompanies} setSelectedValue = {props.setSelectedValueСompanies}/></div>
        </div>
    );
}