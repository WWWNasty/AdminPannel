const SecondStep = (props) => {
    return(
        <div>
            <MyMultipleSelect selectOptions = {props.selectOptions} selectedValue = {props.selectedValue} setSelectedValue = {props.setSelectedValue} selectName = "Выберите объекты" />
            <FormDialogObject selectOptions = {props.selectOptions} selectedValue = {props.selectedValue} setSelectedValue = {props.setSelectedValue}/>
        </div>
    );
}