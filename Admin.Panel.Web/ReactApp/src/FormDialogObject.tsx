interface TypeObjectProperties extends SelectOption {
    questionaryObjectTypeId: number;
}

const FormDialogObject = (props) => {
    const dialogForm = useForm();
    const form = useFormContext();
    const selectedObjectTypeId = form.watch('objectTypeId');
    const {register, handleSubmit, control, reset, errors, setError} = dialogForm;

    const onSubmit = async data => {
        data.objectTypeId = selectedObjectTypeId;
        data.selectedObjectPropertyValues?.forEach(prop => prop.objectPropertyId = Number(prop.objectPropertyId));

        const response = await fetch("/api/QuestionaryObjectApi", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })

        if (response.ok) {
            const result = await response.json();
            props.setOpenAlertGreen(true);
            props.selectedObjectype.questionaryObjects.push(result);
            debugger;
            setOpen(false);
        } else if (response.status == 400) {
            const type = 'oneOrMoreRequired';
            setError('code', {type, message: 'Введите уникальный код!'});
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
    const objectTypeId = form.watch('objectTypeId');
    const objectType = props.objectTypes?.find(o => o.id == objectTypeId);
    
    return (
        <div>
            <Button variant="outlined" color="primary" onClick={handleClickOpen} className="mt-3 mb-2">
                Создать новый объект
            </Button>
            <Dialog fullWidth={true} maxWidth={'lg'} open={open} onClose={handleClose}
                    aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Новый объект</DialogTitle>
                <form autoComplete="off">
                    <DialogContent>
                        <Controller
                            error={errors?.name?.type}
                            as={TextField}
                            autoFocus
                            name="name"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Название объекта"
                            fullWidth={true}
                            rules={{required: true, maxLength: {message:'Максимально символов: 250', value:250}, validate: true}}
                            helperText={errors?.name?.message}
                        />

                        <Controller
                            error={errors?.code?.type}
                            as={TextField}
                            name="code"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Код"
                            fullWidth={true}
                            rules={{required: true, maxLength: {message:'Максимально символов: 20', value:20}, validate: true}}
                            helperText={errors?.code?.message}
                        />
                        
                        <Controller
                            error={errors?.description?.type}
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
                            rules={{required: true, maxLength: {message:'Максимально символов: 500', value:500}, validate: true}}
                            helperText={errors?.description?.message}
                        />

                        <div className="font-weight-light mt-3" style={{color: '#3f51b5'}}>Заполните свойства типа объекта "{objectType.name}": </div>
                        {props.selectedObjectype.objectProperties?.map((item, index) =>
                            <div>
                                <Controller
                                    error={errors?.selectedObjectPropertyValues?.[index]?.value?.type}
                                    as={TextField}
                                    name={`selectedObjectPropertyValues[${index}].value`}
                                    control={control}
                                    defaultValue=""
                                    required
                                    margin="dense"
                                    id="standard-required"
                                    label={item.name}
                                    fullWidth={true}
                                    rules={{required: true, maxLength: {message:'Максимально символов: 250', value:250}, validate: true}}
                                    helperText={errors?.selectedObjectPropertyValues?.[index]?.value?.message}
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
                        <Button onClick={handleSubmit(onSubmit)} color="primary">
                            Добавить
                        </Button>
                    </DialogActions>
                </form>
            </Dialog>
        </div>
    );
}