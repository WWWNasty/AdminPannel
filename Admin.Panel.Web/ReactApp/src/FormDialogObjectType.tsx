function FormDialogObjectType(props) {
    const dialogForm = useForm();
    const {register, handleSubmit, control, reset, errors} = dialogForm;

    const onSubmit = async data => {
        const response = await fetch("/api/ObjectTypeApi", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })
        
        const newObjectType = await response.json();

        props.setObjectTypes([newObjectType, ...props.selectOptionsTypes]);
        reset();
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
                <form autoComplete="off">
                    <DialogContent>
                        <div>
                            <MySelect
                                error={errors?.companyId?.type}
                                autoFocus
                                required={{message: '', value: true}}
                                name="companyId" 
                                selectOptions={props.selectOptions}
                                selectedValue={props.selectedValue}
                                form={dialogForm}
                                setSelectedValue={props.setSelectedValue}
                                nameSwlect="Выберите компанию"/>
                        </div>
                        <Controller
                            error={errors?.name?.type}
                            as={TextField}
                            name="name"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Название типа объекта"
                            fullWidth={true}
                            rules={{required: true, maxLength: {message:'Максимально символов: 250', value:250}, validate: true}}
                            helperText={errors?.name?.message}
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
                                <Icon>add</Icon> <h6 className="mt-2 font-weight-light">Добавить свойство</h6>
                            </IconButton>
                        </div>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose} color="primary">
                            Отмена
                        </Button>
                        <Button onClick={handleSubmit(onSubmit)} color="primary">
                            Добавить
                        </Button>
                    </DialogActions>
                </form>
            </Dialog>
        </div>
    );
}