function FormDialogObjectType(props) {
    const dialogForm = useForm();
    const {register, handleSubmit, control, reset} = dialogForm;

    const onSubmit = async data => {
        console.log(data);
        const response = await fetch("/api/ObjectTypeApi", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })
        
        const newObjectType = await response.json();
        console.log(newObjectType);

        debugger;
        props.setObjectTypes([newObjectType, ...props.selectOptionsTypes]);
        setOpen(false);
        
        if (response.ok) {
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
        reset();
    };
    const objectProperties = `objectProperties`;

    const {remove, append, fields} = useFieldArray({control: dialogForm.control, name: objectProperties});

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
                        <div>
                            <MySelect
                                autoFocus
                                required
                                name="companyId" 
                                selectOptions={props.selectOptions}
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
                            {fields.map((property, index) => {
                                return (
                                    <CardProp
                                        remove={() => remove(index)}
                                        key={property.key}
                                        index={index}
                                        form={dialogForm}
                                        registerForm={register}/>
                                ); 
                            })}
                        </div> 

                        <div>
                            <IconButton color="primary" aria-label="add" onClick={() => append({
                                key: Math.random(),
                                name: '',
                                isUsedInReport: false,
                                nameInReport: ''
                            })
                            }>
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