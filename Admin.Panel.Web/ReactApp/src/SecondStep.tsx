const SecondStep = (props) => {
    return(
        <div>
            <MyMultipleSelect selectOptions = {props.selectOptions} selectedValue = {props.selectedValue} setSelectedValue = {props.setSelectedValue}/>
            <MySelect selectOptions = {props.selectOptions} selectedValue = {props.selectedValue} setSelectedValue = {props.setSelectedValue} nameSwlect = "Выберите объекты" />
            <FormDialogObject selectOptions = {props.selectOptions} selectedValue = {props.selectedValue} setSelectedValue = {props.setSelectedValue}/>
        </div>
    );
}