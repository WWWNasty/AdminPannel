interface TypeObjectProperties extends SelectOption {
    questionaryObjectTypeId: number;
}

const FormDialogObject = (props) => {
    const dialogForm = useForm();
    const form = useFormContext();
    const selectedObjectTypeId = form.watch('objectTypeId');

    const {register, handleSubmit, control} = dialogForm;

    const onSubmit = data => {
        data.objectTypeId = selectedObjectTypeId;
        
        data.selectedObjectPropertyValues.forEach(prop => prop.objectPropertyId = Number(prop.objectPropertyId));

        const response = fetch("/api/QuestionaryObjectApi", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })
        
        props.setObjectTypes([data, ...props.selectOptions]);
        setOpen(false);
        if (response) {
            props.setOpenAlertGreen(true);
        } else {
            props.setOpenAlertRed(true);
        }
        
        return false;
    };
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <div>
            <Button variant="outlined" color="primary" onClick={handleClickOpen} className="mt-3 mb-2">
                Создать новый объект
            </Button>
            <Dialog fullWidth={true} maxWidth={'lg'} open={open} onClose={handleClose}
                    aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Новый объект</DialogTitle>
                <form onSubmit={handleSubmit(onSubmit)} autoComplete="off">
                    <DialogContent>
                        <Controller
                            as={TextField}
                            autoFocus
                            name="name"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Название"
                            fullWidth={true}
                        />
                        <Controller
                            as={TextField}
                            name="code"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Код"
                            fullWidth={true}
                        />
                        <Controller
                            as={TextField}
                            name="description"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-multiline-static"
                            label="Описание"
                            fullWidth={true}
                            multiline
                            rows={4}
                        />

                         {props.selectedObjectype.objectProperties?.map((item, index) =>
                            <div>
                                <Controller
                                    as={TextField}
                                    name={`selectedObjectPropertyValues[${index}].value`}
                                    control={control}
                                    defaultValue=""
                                    required
                                    margin="dense"
                                    id="standard-required"
                                    label={item.name}
                                    fullWidth={true}
                                />
                                <input type="hidden" 
                                       ref={register} 
                                       name={`selectedObjectPropertyValues[${index}].objectPropertyId`}
                                       value={item.id}
                                />
                            </div>
                        )}
                        
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