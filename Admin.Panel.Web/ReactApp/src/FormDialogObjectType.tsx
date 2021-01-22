function FormDialogObjectType(props) {
    const [indexes, setIndexes] = React.useState([]);
    const [counter, setCounter] = React.useState(0);
    const dialogForm = useForm();
    const {register, handleSubmit, control} = dialogForm;

    const onSubmit = data => {
        console.log(data);
        const response = fetch("/api/ObjectTypeApi", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })
        props.setObjectTypes([data, ...props.selectOptionsTypes]);
        setOpen(false);
        setCounter(0);
        if (response) {
            props.setOpenAlertGreen(true);
        } else {
            props.setOpenAlertRed(true);
        }
    };
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };
    const addFriend = () => {
        setIndexes(prevIndexes => [...prevIndexes, counter]);
        setCounter(prevCounter => prevCounter + 1);
    };

    return (
        <div>
            <Button variant="outlined" color="primary" onClick={handleClickOpen} className="mt-3 mb-2">
                Создать новый тип объекта
            </Button>
            <Dialog fullWidth={true} maxWidth={'lg'} open={open} onClose={handleClose}
                    aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Новый тип объекта</DialogTitle>
                <form onSubmit={handleSubmit(onSubmit)} autoComplete="off">
                    <DialogContent>
                        <div><MySelect autoFocus required name="companyId" selectOptions={props.selectOptions}
                                       selectedValue={props.selectedValue}
                                       form={dialogForm}
                                       setSelectedValue={props.setSelectedValue}
                                       nameSwlect="Выберите компанию"/>
                        </div>
                        <Controller
                            as={TextField}
                            name="name"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Название"
                            fullWidth={true}
                        />
                        
                        <div>
                            {indexes.map(index => {
                                return (
                                    <CardProp index={index} setIndexes={setIndexes} setCounter={setCounter} form={dialogForm} registerForm={register}/>
                                );
                            })}
                        </div>

                        <div>
                            <IconButton color="primary" aria-label="add" onClick={addFriend}>
                                <Icon>add</Icon>
                            </IconButton>
                        </div>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose} color="primary">
                            Отмена
                        </Button>

                        <Button type="submit" color="primary">
                            Добавить
                        </Button>
                    </DialogActions>
                </form>
            </Dialog>
        </div>
    );
}