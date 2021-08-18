import React from 'react';

/**
 * @param {{ value?: string, children?: any[], submit: () => void}} props
 */
export default function LoginButton({ value, children, submit }) {
  return (
    <button className="login-button" onClick={() => submit()}>
        {value || children}
    </button>
  );
}