import '../styles/loading.css';

export default function Loading() {
  return (
    <div style={overlayStyle}>
      <div style={spinnerStyle} />
    </div>
  )
}

const overlayStyle: React.CSSProperties = {
  position: 'fixed',
  top: 0,
  left: 0,
  width: '100vw',
  height: '100vh',
  background: 'rgba(0,0,0,0.4)',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  zIndex: 9999
}

const spinnerStyle: React.CSSProperties = {
  width: 60,
  height: 60,
  border: '6px solid #ccc',
  borderTop: '6px solid #007bff',
  borderRadius: '50%',
  animation: 'spin 1s linear infinite'
}
