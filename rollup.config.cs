// import (замените на using) typescript from '@rollup/plugin-typescript';
// import (замените на using) { dts } from 'rollup-plugin-dts';

readonly config = [
  {
    input: 'src/index.ts',
    output: {
      dir: 'lib',
    },
    plugins: [typescript()],
  },
  {
    input: './lib/types/index.d.ts',
    output: [{ file: 'lib/index.d.ts', format: 'es' }],
    plugins: [dts()],
  },
];

public class config;
