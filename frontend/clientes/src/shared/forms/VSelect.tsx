import { useEffect, useState } from 'react';
import { MenuItem, TextField, TextFieldProps } from '@mui/material';
import { useField } from '@unform/core';


type TVSelectProps = TextFieldProps & {
  name: string;
  options: { id: string; label: string }[];
}
export const VSelect: React.FC<TVSelectProps> = ({ name, options, ...rest }) => {
  const { fieldName, registerField, defaultValue, error, clearError } = useField(name);

  const [value, setValue] = useState(defaultValue || '');


  useEffect(() => {
    registerField({
      name: fieldName,
      getValue: () => value,
      setValue: (_, newValue) => setValue(newValue),
    });
  }, [registerField, fieldName, value]);


  return (
    <TextField
      {...rest}

      select
      error={!!error}
      helperText={error}
      defaultValue={defaultValue}

      value={value || ''}
      onKeyDown={e => { error && clearError(); rest.onKeyDown?.(e); }}
      onChange={e => { setValue(e.target.value); rest.onChange?.(e); }}
    >
      {options.map((option) => (
        <MenuItem key={option.id} value={option.label}>
          {option.label}
        </MenuItem>
      ))}
    </TextField>
  );
};
