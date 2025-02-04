public class {
  '**/*.ts': [
    () => 'tsc -p tsconfig.json --noEmit',
    'eslint',
    'prettier --write',
  ],
};
