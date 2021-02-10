const SecondStep = (props) => {
    const form = useFormContext();
    const [openAlertGreen, setOpenAlertGreen] = React.useState(false);
    const [openAlertRed, setOpenAlertRed] = React.useState(false);

    const handleClose = () => {
        setOpenAlertGreen(false);
        setOpenAlertRed(false);
    };
    return (
        <div>
            <MyMultipleSelect
                form={form}
                selectOptions={props.selectOptions}
                selectedValue={props.selectedValue}
                selectName="Выберите объекты для анкеты"
            />
            <FormDialogObject
                objectTypes={props.objectTypes}
                selectedObjectype={props.selectedObjectype}
                setOpenAlertRed={setOpenAlertRed}
                setOpenAlertGreen={setOpenAlertGreen}
            />

            <Snackbar open={openAlertGreen} autoHideDuration={6000} onClose={handleClose}>
                <div className="alert alert-success" role="alert">
                    Объект успешно создан!
                    <button type="button" className="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </Snackbar>

            <Snackbar open={openAlertRed} autoHideDuration={6000} onClose={handleClose}>
                <div className="alert alert-danger" role="alert">
                    Произошла ошибка, объект не создан!
                    <button type="button" className="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </Snackbar>
        </div>
    );
}